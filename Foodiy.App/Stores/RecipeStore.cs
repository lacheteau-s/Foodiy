using System.Text.Json;

namespace Foodiy.App.Stores;

public class RecipeStore
{
    public static async Task<IEnumerable<string>> GetRecipesAsync()
    {
        var json = await FileSystem.OpenAppPackageFileAsync("recipes.json");
        var recipes = await JsonSerializer.DeserializeAsync<IEnumerable<string>>(json);

        return recipes!; // TODO: null check when fetching from API;
    }
}
