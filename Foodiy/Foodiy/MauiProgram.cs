using Foodiy.ViewModels;
using Foodiy.Views;

namespace Foodiy;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureServices()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}

	public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
	{
		var services = builder.Services;

		services.AddTransient<HomePage>();
		services.AddSingleton<HomePageViewModel>();

		return builder;
	}
}
