using Foodiy.App.Constants;
using Foodiy.App.Models;

namespace Foodiy.App.ViewModels;

public class RecipePageViewModel : IQueryAttributable
{
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var recipe = query[Parameters.RecipeModelParam] as RecipeModel;
    }
}
