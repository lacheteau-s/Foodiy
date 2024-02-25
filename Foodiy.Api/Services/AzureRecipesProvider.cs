using Azure.Storage.Files.Shares;
using Foodiy.Api.Models;
using System.Text.Json;

namespace Foodiy.Api.Services;

public class AzureRecipesProvider : IRecipesProvider
{
    private const string _configSectionName = "AzureStorage";
    private readonly string _connectionString;
    private readonly string _shareName;

    public AzureRecipesProvider(IConfiguration configuration)
    {
        if (configuration == null)
            throw new ArgumentNullException(nameof(configuration));

        var section = configuration.GetSection(_configSectionName);

        _connectionString = section.GetValue<string>("ConnectionString");

        if (_connectionString == null)
            throw new InvalidOperationException("Missing configuration entry for AzureStorage:ConnectionString");

        _shareName = section.GetValue<string>("ShareName");

        if (_shareName == null)
            throw new InvalidOperationException("Missing configuration entry for AzureStorage:ShareName");
    }

    public async Task<IEnumerable<RecipeDetailsModel>> GetRecipesAsync(CancellationToken cancellationToken = default)
    {
        var fileShare = new ShareClient(_connectionString, _shareName);

        var directory = await fileShare.ExistsAsync(cancellationToken)
            ? fileShare.GetRootDirectoryClient()
            : throw new DirectoryNotFoundException(fileShare.Uri.ToString());

        var file = await directory.ExistsAsync(cancellationToken)
            ? directory.GetFileClient("recipes.json")
            : throw new DirectoryNotFoundException(directory.Uri.ToString());

        var response = await file.ExistsAsync(cancellationToken)
            ? await file.DownloadAsync(cancellationToken: cancellationToken)
            : throw new FileNotFoundException(file.Uri.ToString());

        var recipes = await JsonSerializer.DeserializeAsync<IEnumerable<RecipeDetailsModel>>(response.Value.Content, cancellationToken: cancellationToken);

        return recipes ?? throw new IOException("Failed to read content from file");
    }
}
