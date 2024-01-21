using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.App.Stores;

namespace Foodiy.App.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly RecipeStore _recipeStore;

    [ObservableProperty]
    private IEnumerable<string> _recipes;

    public HomePageViewModel(RecipeStore recipeStore)
    {
        _recipeStore = recipeStore;
        _recipes = Enumerable.Empty<string>();
    }

    [RelayCommand]
    public async Task InitializeAsync()
    {
        Recipes = await _recipeStore.GetRecipesAsync();
    }
}
