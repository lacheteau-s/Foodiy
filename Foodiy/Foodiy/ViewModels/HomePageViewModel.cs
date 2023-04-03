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
        private void LoadRecipes()
        {
            Recipes = new(_recipeRepository.GetRecipes());
        }

        [RelayCommand]
        private async Task AddNewRecipe()
        {
            // await Shell.Current.GoToAsync("NewRecipePage");

            var id = (Recipes.Any() ? Recipes.Max(r => r.Id) : 0) + 1;
            var recipe = new RecipeModel(id, $"Recipe {id}");

            _recipeRepository.AddRecipe(recipe);
            Recipes.Add(recipe);
        }

        [RelayCommand]
        private void RemoveRecipe(RecipeModel recipe)
        {
            _recipeRepository.RemoveRecipe(recipe);
            Recipes.Remove(recipe);
        }
    }
}
