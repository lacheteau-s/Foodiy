﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.App.Constants;
using Foodiy.App.Helpers;
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
        var param = new Dictionary<string, object> { [Parameters.RecipeModelParam] = recipe };

        await NavigationHelper.NavigateTo<RecipePageViewModel>(param);
    }
}
