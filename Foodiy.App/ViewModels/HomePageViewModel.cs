using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Foodiy.App.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    public IEnumerable<string> Recipes { get; init; }

    public HomePageViewModel()
    {
        Recipes = new[]
        {
            "Chilli con carne",
            "Burritos",
            "Salmon with avocado salsa",
            "Tex-Mex chicken and rice",
            "Mac & cheese",
            "Banana bread muffins",
            "Pasta al pesto",
            "Mushrooms omelet",
            "Overnight oats"
        };
    }
}
