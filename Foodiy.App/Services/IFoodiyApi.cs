using Foodiy.App.Models;
using Refit;

namespace Foodiy.App.Services;

public interface IFoodiyApi
{
    [Get("/api/recipes")]
    Task<IEnumerable<RecipeModel>> GetRecipes();

    [Get("/api/recipes/{id}")]
    Task<RecipeModel> GetRecipe(int id);
}
