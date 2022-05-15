using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Maui.LifecycleEvents;
using Workoutisten.FitStreak.Data;
using MudBlazor.Services;
using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Converter;
using Workoutisten.FitStreak.Data.Models.User;
using Workoutisten.FitStreak.Converter.User;
using Workoutisten.FitStreak.Data.Converter;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

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

        //RestClient
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<IRestClient, RestClient>(Services => 
        {
            return new RestClient("https://localhost:7228", Services.GetRequiredService<IHttpClientFactory>().CreateClient());
        });

        //Converters
        builder.Services.AddTransient<IConverterWrapper, ConverterWrapper>();
        builder.Services.AddSingleton<IConverter<RegisterModel, RegistrationRequest>, RegisterConverter>();

#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                    WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                    AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);

                    const int width = 480;
                    const int height = 853;
                    winuiAppWindow.MoveAndResize(new RectInt32(1920 / 2 - width / 2, 1080 / 2 - height / 2, width, height));
                });
            });
        });
#endif

        return builder.Build();
	}
}
