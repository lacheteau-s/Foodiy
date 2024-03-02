using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.App.Constants;
using Foodiy.App.Helpers;
using Foodiy.App.Models;
using Foodiy.App.Services;
using Foodiy.App.Stores;
using System.Net.Http.Json;

namespace Foodiy.App.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly RecipeStore _recipeStore;

    private readonly IFoodiyApi _api;

    [ObservableProperty]
    private IEnumerable<RecipeModel> _recipes;

    public HomePageViewModel(RecipeStore recipeStore, IFoodiyApi api)
    {
        _recipeStore = recipeStore;
        _recipes = Enumerable.Empty<RecipeModel>();
        _api = api;
    }

    [RelayCommand]
    public async Task InitializeAsync()
    {
        var recipes = await _api.GetRecipes();
        
        Recipes = recipes;
    }

    [RelayCommand]
    public async Task OpenRecipe(RecipeModel recipe)
    {
        var param = new Dictionary<string, object> { [Parameters.RecipeModelParam] = recipe };

        await NavigationHelper.NavigateTo<RecipePageViewModel>(param);
    }
}
