using Foodiy.Api.Models;
using System.Text.Json;

namespace Foodiy.Api.Services;

public class RecipesService : IRecipesService
{
    public async Task<IEnumerable<RecipeModel>> GetRecipesAsync(CancellationToken cancellationToken = default)
    {
        var recipes = await LoadRecipesFromFileAsync(cancellationToken);

        return recipes;
    }

    public async Task<RecipeModel?> GetRecipeAsync(int id, CancellationToken cancellationToken = default)
    {
        var recipes = await LoadRecipesFromFileAsync(cancellationToken);
        var recipe = recipes?.FirstOrDefault(x => x.Id == id);

        return recipe;
    }

    private static async Task<IEnumerable<RecipeModel>> LoadRecipesFromFileAsync(CancellationToken cancellationToken = default)
    {
        using var stream = File.OpenRead("Data/recipes.json");
        var recipes = await JsonSerializer.DeserializeAsync<IEnumerable<RecipeModel>>(stream, cancellationToken: cancellationToken);

        return recipes ?? throw new IOException("Failed to load contents from recipes.json");
    }
}
