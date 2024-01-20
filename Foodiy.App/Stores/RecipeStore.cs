namespace Foodiy.App.Stores;

public class RecipeStore
{
    public static IEnumerable<string> GetRecipes() => new[]
    {
        "Chilli con carne",
        "Burritos",
        "Salmon with avocado salsa",
        "Tex mex chicken and rice",
        "Mac & cheese",
        "Banana bread muffins",
        "Pasta al pesto",
        "Mushrooms omelet",
        "Overnight oats"
    };
}
