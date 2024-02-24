using Foodiy.Api.Models;

namespace Foodiy.Api.Services;

public interface IRecipesService
{
    Task<IEnumerable<RecipeSummaryModel>> GetRecipesAsync(CancellationToken cancellationToken = default);

    Task<RecipeDetailsModel?> GetRecipeAsync(int id, CancellationToken cancellationToken = default);
}
