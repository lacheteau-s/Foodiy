using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.App.Stores;

namespace Foodiy.App.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    [ObservableProperty]
    private IEnumerable<string> _recipes;

    public HomePageViewModel()
    {
        _recipes = Enumerable.Empty<string>();
    }

    [RelayCommand]
    public async Task InitializeAsync()
    {
        Recipes = await RecipeStore.GetRecipesAsync();
    }
}
