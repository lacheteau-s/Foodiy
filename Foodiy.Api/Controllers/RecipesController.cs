using Microsoft.AspNetCore.Mvc;

namespace Foodiy.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IEnumerable<string> _recipes = new List<string>
    {
        "Recipe 1",
        "Recipe 2",
        "Recipe 3",
        "Recipe 4",
        "Recipe 5",
    };

    [HttpGet]
    public IEnumerable<string> GetRecipes()
    {
        return _recipes;
    }

    [HttpGet("{id}")]
    public IActionResult GetRecipe(int id)
    {
        var recipe = _recipes.ElementAtOrDefault(id);

        if (recipe == null)
            return NotFound();

        return Ok(recipe);
    }
}
