using Foodiy.Api.Models;

namespace Foodiy.Api.Services;

public class RecipesService : IRecipesService
{
    private readonly IRecipesProvider _recipesProvider;

    public RecipesService(IRecipesProvider recipesProvider)
    {
        _recipesProvider = recipesProvider ?? throw new ArgumentNullException(nameof(recipesProvider));
    }

    public async Task<IEnumerable<RecipeSummaryModel>> GetRecipesAsync(CancellationToken cancellationToken = default)
    {
        var recipes = await _recipesProvider.GetRecipesAsync(cancellationToken);
        var summaries = recipes.Select(r => new RecipeSummaryModel(r.Id, r.Name, r.ThumbnailUrl));

        return summaries;
    }

    public async Task<RecipeDetailsModel?> GetRecipeAsync(int id, CancellationToken cancellationToken = default)
    {
        var recipes = await _recipesProvider.GetRecipesAsync(cancellationToken);
        var recipe = recipes?.FirstOrDefault(x => x.Id == id);

        return recipe;
    }
}
