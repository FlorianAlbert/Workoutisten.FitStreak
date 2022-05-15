using Microsoft.AspNetCore.Components.WebView.Maui;
using Workoutisten.FitStreak.Data;
using MudBlazor.Services;

namespace Workoutisten.FitStreak;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		
		builder.Services.AddMudServices();

		return builder.Build();
	}
}
