using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.App.Constants;
using Foodiy.App.Helpers;
using Foodiy.App.Models;
using Foodiy.App.Services;

namespace Foodiy.App.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly IFoodiyApi _api;

    [ObservableProperty]
    private IEnumerable<RecipeSummaryModel> _recipes;

    public HomePageViewModel(IFoodiyApi api)
    {
        _recipes = Enumerable.Empty<RecipeSummaryModel>();
        _api = api;
    }

    [RelayCommand]
    public async Task InitializeAsync()
    {
        // TODO: caching / local storage
        var recipes = await _api.GetRecipes();
        
        Recipes = recipes;
    }

    [RelayCommand]
    public async Task OpenRecipe(RecipeSummaryModel recipeSummary)
    {
        var recipeDetails = await _api.GetRecipe(recipeSummary.Id); // TODO: error management
        // TODO: find a way to load from the details page
        var param = new Dictionary<string, object> { [Parameters.RecipeDetailsModelParam] = recipeDetails };

        await NavigationHelper.NavigateTo<RecipePageViewModel>(param);
    }
}
