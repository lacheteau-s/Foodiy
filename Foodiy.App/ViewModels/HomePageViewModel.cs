using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.App.Constants;
using Foodiy.App.Helpers;
using Foodiy.App.Models;
using Foodiy.App.Stores;
using System.Net.Http.Json;

namespace Foodiy.App.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly RecipeStore _recipeStore;

    private readonly IHttpClientFactory _httpClientFactory;

    [ObservableProperty]
    private IEnumerable<RecipeModel> _recipes;

    public HomePageViewModel(RecipeStore recipeStore, IHttpClientFactory httpClientFactory)
    {
        _recipeStore = recipeStore;
        _recipes = Enumerable.Empty<RecipeModel>();
        _httpClientFactory = httpClientFactory;
    }

    [RelayCommand]
    public async Task InitializeAsync()
    {
        using var httpClient = _httpClientFactory.CreateClient("FoodiyApi");
        var response = await httpClient.GetAsync("/api/recipes");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<IEnumerable<RecipeModel>>();
            Recipes = content!; // TODO: null-check
        }
        //Recipes = await _recipeStore.GetRecipesAsync();
    }

    [RelayCommand]
    public async Task OpenRecipe(RecipeModel recipe)
    {
        var param = new Dictionary<string, object> { [Parameters.RecipeModelParam] = recipe };

        await NavigationHelper.NavigateTo<RecipePageViewModel>(param);
    }
}
