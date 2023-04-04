using CommunityToolkit.Maui;
using Foodiy.Repositories;
using Foodiy.ViewModels;
using Foodiy.Views;
using SQLite;

namespace Foodiy;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureServices()
			.UseMauiCommunityToolkit()
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

		services.AddTransient<NewRecipePage>();
		services.AddTransient<NewRecipePageViewModel>();

		services.AddSingleton(sp =>
		{
			var dbPath = Path.Combine(FileSystem.AppDataDirectory, "foodiy.db3");
			var flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite;

			return new SQLiteAsyncConnection(dbPath, flags);
		});

		services.AddSingleton<RecipeRepository>();

		return builder;
	}
}
