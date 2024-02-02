using CommunityToolkit.Mvvm.ComponentModel;
using Foodiy.App.Constants;
using Foodiy.App.Models;

namespace Foodiy.App.ViewModels;

public partial class RecipePageViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private string _recipeName = string.Empty;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var recipe = query[Parameters.RecipeModelParam] as RecipeModel;

        if (recipe != null)
        {
            RecipeName = recipe.Name;
        }
    }
}
