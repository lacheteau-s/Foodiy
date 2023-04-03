using Foodiy.Models;

namespace Foodiy.Repositories
{
    public class RecipeRepository
    {
        private static readonly List<RecipeModel> _recipes = new()
        {
            new RecipeModel(1, "Recipe 1"),
            new RecipeModel(2, "Recipe 2")
        };

        public IEnumerable<RecipeModel> GetRecipes() => _recipes.ToList();

        public void AddRecipe(RecipeModel recipe)
        {
            _recipes.Add(recipe);
        }

        public void RemoveRecipe(RecipeModel recipe)
        {
            _recipes.Remove(recipe);
        }
    }
}
