using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.Models;
using Foodiy.Repositories;
using System.Collections.ObjectModel;

namespace Foodiy.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly RecipeRepository _recipeRepository;

        [ObservableProperty]
        private ObservableCollection<RecipeModel> _recipes = new();

        public HomePageViewModel(RecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
        }

        [RelayCommand]
        private async Task LoadRecipes()
        {
            var recipes = await _recipeRepository.GetRecipes();

            Recipes = new(recipes);
        }

        [RelayCommand]
        private async Task AddNewRecipe()
        {
            await Shell.Current.GoToAsync("NewRecipePage");
        }

        [RelayCommand]
        private async Task RemoveRecipe(RecipeModel recipe)
        {
            await _recipeRepository.RemoveRecipe(recipe.Id);
            await LoadRecipes();
        }
    }
}
