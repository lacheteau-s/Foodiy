using Foodiy.ViewModels;

namespace Foodiy.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		BindingContext = viewModel;

		InitializeComponent();
	}
}

