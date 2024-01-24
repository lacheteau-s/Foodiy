using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.App.Models;
using Foodiy.App.Stores;

namespace Foodiy.App.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly RecipeStore _recipeStore;

    [ObservableProperty]
    private IEnumerable<RecipeModel> _recipes;

    public HomePageViewModel(RecipeStore recipeStore)
    {
        _recipeStore = recipeStore;
        _recipes = Enumerable.Empty<RecipeModel>();
    }

    [RelayCommand]
    public async Task InitializeAsync()
    {
        Recipes = await _recipeStore.GetRecipesAsync();
    }

    [RelayCommand]
    public async Task OpenRecipe(RecipeModel recipe)
    {
        await Shell.Current.GoToAsync(nameof(RecipePageViewModel));
    }
}
