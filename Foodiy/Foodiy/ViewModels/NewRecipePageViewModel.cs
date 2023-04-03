using CommunityToolkit.Mvvm.ComponentModel;

namespace Foodiy.ViewModels;

public partial class NewRecipePageViewModel : ObservableObject
{
    public string Title => "New recipe";

    [ObservableProperty]
    private string _name = string.Empty;
}
