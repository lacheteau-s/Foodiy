namespace Foodiy.App.Models;

public record RecipeDetailsModel(
    int Id,
    string Name,
    string ThumbnailUrl,
    IEnumerable<IngredientModel> Ingredients,
    IEnumerable<string> Steps);

public record IngredientModel(string Name, string Quantity);