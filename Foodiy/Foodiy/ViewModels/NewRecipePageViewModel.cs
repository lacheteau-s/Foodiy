using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Foodiy.Repositories;

namespace Foodiy.ViewModels;

public partial class NewRecipePageViewModel : ObservableObject
{
    private readonly RecipeRepository _recipeRepository;

    public string Title => "New recipe";

    [ObservableProperty]
    private string _name = string.Empty;

    public NewRecipePageViewModel(RecipeRepository recipeRepository)
    {
        _recipeRepository = recipeRepository ?? throw new ArgumentNullException(nameof(recipeRepository));
    }

    [RelayCommand]
    private async Task SaveRecipe()
    {
        await _recipeRepository.AddRecipe(Name);

        await Shell.Current.GoToAsync("..", true);
    }
}
