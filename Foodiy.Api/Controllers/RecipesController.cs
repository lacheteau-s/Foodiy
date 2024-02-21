using Foodiy.Api.Models;
using Foodiy.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Foodiy.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IRecipesService _recipesService;

    public RecipesController(IRecipesService recipesService)
    {
        _recipesService = recipesService ?? throw new ArgumentNullException(nameof(recipesService));
    }

    [HttpGet]
    public async Task<IEnumerable<RecipeSummaryModel>> GetRecipes(CancellationToken cancellationToken = default)
    {
        var recipes = await _recipesService.GetRecipesAsync(cancellationToken);

        return recipes;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipe(int id, CancellationToken cancellationToken = default)
    {
        var recipe = await _recipesService.GetRecipeAsync(id, cancellationToken);

        if (recipe == null)
            return NotFound();

        return Ok(recipe);
    }
}
