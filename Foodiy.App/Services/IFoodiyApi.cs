using Foodiy.App.Models;
using Refit;

namespace Foodiy.App.Services;

public interface IFoodiyApi
{
    [Get("/api/recipes")]
    Task<IEnumerable<RecipeSummaryModel>> GetRecipes();

    [Get("/api/recipes/{id}")]
    Task<RecipeDetailsModel> GetRecipe(int id);
}
