using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.LifecycleEvents;
using MudBlazor.Services;
using System.Reflection;
using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Converter;
using Workoutisten.FitStreak.Data.Converter;
using Workoutisten.FitStreak.Data.Converter.User;
using Workoutisten.FitStreak.Data.Models.User;
using Workoutisten.FitStreak.Services;

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

        //Load Configuration
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("Workoutisten.FitStreak.Properties.launchSettings.json");
        var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();
        builder.Configuration.AddConfiguration(config);

        //HttpClient
        HttpClient httpClient = null;

#if __ANDROID__
        httpClient = new HttpClient(new Xamarin.Android.Net.AndroidMessageHandler());
#endif


        //RestClient
        builder.Services.AddHttpClient();
        builder.Services.AddSingleton<IRestClient, RestClient>(Services =>
        {
            var configSection = builder.Configuration.GetRequiredSection("LoginConfiguration");
            //return new RestClient($"{configSection.GetSection("BaseUri")}:{configSection.GetSection("Port")}", Services.GetRequiredService<IHttpClientFactory>().CreateClient());

            if (httpClient is null)
            {
                httpClient = Services.GetRequiredService<IHttpClientFactory>().CreateClient();
            }


            return new RestClient($"https://localhost:7228", httpClient);
        });


        //Converters
        builder.Services.AddTransient<IConverterWrapper, ConverterWrapper>();
        builder.Services.AddSingleton<IConverter<RegisterModel, RegistrationRequest>, RegisterConverter>();
        builder.Services.AddSingleton<IConverter<LoginModel, AuthenticationRequest>, LoginConverter>();

        //Authentication
        builder.Services.AddSingleton<AuthenticationTokenHolderModel>();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());

        //PushNotificationManager
        builder.Services.AddSingleton<IPushNotificationManager, PushNotificationManager>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

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
