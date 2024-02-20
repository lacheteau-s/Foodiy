namespace Foodiy.Api.Models;

public record RecipeModel(
    int Id,
    string Name,
    string ThumbnailUrl,
    IEnumerable<IngredientModel> Ingredients,
    IEnumerable<string> Steps);

public record IngredientModel(string Name, string Quantity);
