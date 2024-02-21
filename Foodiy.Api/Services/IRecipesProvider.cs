using Foodiy.Api.Models;

namespace Foodiy.Api.Services;

public interface IRecipesProvider
{
    Task<IEnumerable<RecipeModel>> GetRecipesAsync(CancellationToken cancellationToken = default);
}
