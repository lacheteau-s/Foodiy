using Foodiy.App.ViewModels;

namespace Foodiy.App.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
	}
}

