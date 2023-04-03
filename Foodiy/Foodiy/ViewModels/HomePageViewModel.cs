using CommunityToolkit.Mvvm.Input;

namespace Foodiy.ViewModels
{
    public partial class HomePageViewModel
    {
        [RelayCommand]
        private async Task AddNewRecipe()
        {
            await Shell.Current.GoToAsync("NewRecipePage");
        }
    }
}
