using Azure.Storage.Files.Shares;
using Foodiy.Api.Configuration.Options;
using Foodiy.Api.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Foodiy.Api.Services;

public class AzureRecipesProvider : IRecipesProvider
{
    private readonly AzureStorageOptions _configuration;

    public AzureRecipesProvider(IOptions<AzureStorageOptions> storageConfig)
    {
        _configuration = storageConfig.Value;
    }

    public async Task<IEnumerable<RecipeDetailsModel>> GetRecipesAsync(CancellationToken cancellationToken = default)
    {
        var fileShare = new ShareClient(_configuration.ConnectionString, _configuration.ShareName);

        var directory = await fileShare.ExistsAsync(cancellationToken)
            ? fileShare.GetRootDirectoryClient()
            : throw new DirectoryNotFoundException(fileShare.Uri.ToString());

        var file = await directory.ExistsAsync(cancellationToken)
            ? directory.GetFileClient(_configuration.FileName)
            : throw new DirectoryNotFoundException(directory.Uri.ToString());

        var response = await file.ExistsAsync(cancellationToken)
            ? await file.DownloadAsync(cancellationToken: cancellationToken)
            : throw new FileNotFoundException(file.Uri.ToString());

        var recipes = await JsonSerializer.DeserializeAsync<IEnumerable<RecipeDetailsModel>>(response.Value.Content, cancellationToken: cancellationToken);

        return recipes ?? throw new IOException("Failed to read content from file");
    }
}
