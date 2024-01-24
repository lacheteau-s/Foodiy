﻿using CommunityToolkit.Maui;
using Foodiy.App.Configuration;
using Foodiy.App.Stores;
using Foodiy.App.ViewModels;
using Foodiy.App.Views;

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

		return builder;
	}
}
