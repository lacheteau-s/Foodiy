using System.Text.Json;

namespace Foodiy.App.Stores;

public class RecipeStore
{
    private IEnumerable<string> _recipes = Enumerable.Empty<string>();

    private readonly Task _initializer;

    public RecipeStore()
    {
        _initializer = InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        // Null check is necessary since InitializeAsync is called before the Task is assigned to _initializer
        if (_initializer?.IsCompleted ?? false) return;

        var json = await FileSystem.OpenAppPackageFileAsync("recipes.json");
        var recipes = await JsonSerializer.DeserializeAsync<IEnumerable<string>>(json);

        _recipes = recipes!; // TODO: null check when fetching from API;
    }

    public async Task<IEnumerable<string>> GetRecipesAsync()
    {
        await _initializer;

        return _recipes;
    }
}
