using Foodiy.ViewModels;

namespace Foodiy.Views;

public partial class NewRecipePage : ContentPage
{
	public NewRecipePage(NewRecipePageViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
	}
}