using CommunityToolkit.Maui;
using Foodiy.App.Configuration;
using Foodiy.App.Services;
using Foodiy.App.Stores;
using Foodiy.App.ViewModels;
using Foodiy.App.Views;
using Refit;

namespace Foodiy.App;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureRouting()
			.ConfigureServices()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("MaterialDesignIcons.ttf", "MDI");
			});

		return builder.Build();
	}

	public static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
	{
		var services = builder.Services;

		services.AddTransient<HomePage>();
		services.AddSingleton<HomePageViewModel>();
		services.AddTransient<RecipePage>();
		services.AddTransient<RecipePageViewModel>();

		services.AddSingleton<RecipeStore>();

		services.AddRefitClient<IFoodiyApi>(new RefitSettings
		{
            // https://www.youtube.com/watch?v=D6HaMHw9hzc&ab_channel=AbhayPrince
            // https://www.youtube.com/watch?v=-Wj1JYkgWNU&t=0s&ab_channel=AbhayPrince
            // https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/local-web-services?view=net-maui-8.0
            HttpMessageHandlerFactory = AndroidHttpsMessageHandler.Get // TODO: only needed for debug?
        })
		.ConfigureHttpClient(httpClient =>
		{
			httpClient.BaseAddress = new Uri("https://10.0.2.2:7066");
		});

		return builder;
	}
}
