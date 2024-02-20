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
    public IEnumerable<string> GetRecipes()
    {
        return _recipesService.GetRecipes();
    }

    [HttpGet("{id}")]
    public IActionResult GetRecipe(int id)
    {
        var recipe = _recipesService.GetRecipe(id);

        if (recipe == null)
            return NotFound();

        return Ok(recipe);
    }
}
