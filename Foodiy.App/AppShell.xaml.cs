using Foodiy.App.Views;

namespace Foodiy.App;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute("Recipe", typeof(RecipePage));
	}
}
