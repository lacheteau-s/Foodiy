using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.App.Stores;

namespace Foodiy.App.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    public IEnumerable<string> Recipes { get; init; }

    public HomePageViewModel()
    {
        Recipes = RecipeStore.GetRecipes();
    }
}
