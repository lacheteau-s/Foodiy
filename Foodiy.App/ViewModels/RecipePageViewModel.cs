using CommunityToolkit.Mvvm.ComponentModel;
using Foodiy.App.Constants;
using Foodiy.App.Models;

namespace Foodiy.App.ViewModels;

public partial class RecipePageViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private string _recipeName = string.Empty;

    [ObservableProperty]
    private string _recipeImageUrl = string.Empty;

    [ObservableProperty]
    private IEnumerable<string> _ingredients = Enumerable.Empty<string>();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var recipe = query[Parameters.RecipeModelParam] as RecipeModel;

        if (recipe != null)
        {
            RecipeName = recipe.Name;
            RecipeImageUrl = recipe.ThumbnailUrl;
            Ingredients = recipe.Ingredients.Select(x => $"{x.Quantity} {x.Name}").ToList();
        }
    }
}
