namespace Foodiy.Api.Services;

public interface IRecipesService
{
    IEnumerable<string> GetRecipes();

    string? GetRecipe(int id);
}
