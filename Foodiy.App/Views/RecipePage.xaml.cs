using Foodiy.App.ViewModels;

namespace Foodiy.App.Views;

public partial class RecipePage : ContentPage
{
	public RecipePage(RecipePageViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
	}
}