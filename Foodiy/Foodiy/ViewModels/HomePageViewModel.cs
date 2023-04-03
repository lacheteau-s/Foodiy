using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.Models;
using System.Collections.ObjectModel;

namespace Foodiy.ViewModels
{
    public partial class HomePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<RecipeModel> _recipes = new()
        {
            new RecipeModel(1, "Recipe 1"),
            new RecipeModel(2, "Recipe 2")
        };

        [RelayCommand]
        private async Task AddNewRecipe()
        {
            // await Shell.Current.GoToAsync("NewRecipePage");

            var id = (Recipes.Any() ? Recipes.Max(r => r.Id) : 0) + 1;
            var recipe = new RecipeModel(id, $"Recipe {id}");

            Recipes.Add(recipe);
        }

        [RelayCommand]
        private void RemoveRecipe(RecipeModel recipe)
        {
            Recipes.Remove(recipe);
        }
    }
}
