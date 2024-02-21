using Foodiy.Api.Models;

namespace Foodiy.Api.Services;

public interface IRecipesProvider
{
    Task<IEnumerable<RecipeDetailsModel>> GetRecipesAsync(CancellationToken cancellationToken = default);
}
