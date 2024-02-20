using Foodiy.Api.Models;

namespace Foodiy.Api.Services;

public interface IRecipesService
{
    Task<IEnumerable<RecipeModel>> GetRecipesAsync(CancellationToken cancellationToken = default);

    Task<RecipeModel?> GetRecipeAsync(int id, CancellationToken cancellationToken = default);
}
