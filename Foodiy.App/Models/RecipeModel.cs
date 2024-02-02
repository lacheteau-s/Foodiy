namespace Foodiy.App.Models;

public record RecipeModel(string Name, string ThumbnailUrl, IEnumerable<IngredientModel> Ingredients);

public record IngredientModel(string Name, string Quantity);