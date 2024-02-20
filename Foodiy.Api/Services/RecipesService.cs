namespace Foodiy.Api.Services;

public class RecipesService : IRecipesService
{
    private readonly IEnumerable<string> _recipes = new List<string>
    {
        "Recipe 1",
        "Recipe 2",
        "Recipe 3",
        "Recipe 4",
        "Recipe 5",
    };

    public IEnumerable<string> GetRecipes() => _recipes;

    public string? GetRecipe(int id) => _recipes.ElementAtOrDefault(id);
}
