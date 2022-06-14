using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.LifecycleEvents;
using MudBlazor.Services;
using System.Reflection;
using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Converter;
using Workoutisten.FitStreak.Data.Converter;
using Workoutisten.FitStreak.Data.Converter.ExerciseAndWorkout;
using Workoutisten.FitStreak.Data.Converter.User;
using Workoutisten.FitStreak.Data.Models.User;
using Workoutisten.FitStreak.Data.Models.Workout;
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

        //Load Configuration (Muss maybe wieder raus, da wir sie sowieso nicht verwenden können)
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

            return new RestClient($"https://fitstreak.de", httpClient, Services.GetRequiredService<CustomAuthenticationStateProvider>());
        });
        
        //Converters
        builder.Services.AddTransient<IConverterWrapper, ConverterWrapper>();
        builder.Services.AddSingleton<IConverter<RegisterModel, RegistrationRequest>, RegisterConverter>();
        builder.Services.AddSingleton<IConverter<LoginModel, AuthenticationRequest>, LoginConverter>();
        builder.Services.AddSingleton<IConverter<ExerciseModel, Exercise>, ExerciseConverter>();
        builder.Services.AddSingleton<IConverter<WorkoutModel, Workout>, WorkoutConverter>();
        builder.Services.AddSingleton<IConverter<ResetPasswordModel, ResetPassword>, ResetPasswordConverter>();
        builder.Services.AddSingleton<IConverter<UserModel, User>, UserConverter>();
        builder.Services.AddSingleton<IConverter<DoneExerciseModel, DoneExercise>, DoneExerciseConverter>();
        builder.Services.AddSingleton<IConverter<ExerciseGroupModel, ExerciseGroup>, ExerciseGroupConverter>();
        builder.Services.AddSingleton<IConverter<StrengthExerciseSetModel, StrengthSet>, StrengthSetConverter>();
        builder.Services.AddSingleton<IConverter<CardioExerciseSetModel, CardioSet>, CardioSetConverter>();

        //Authentication
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());

        //PushNotificationManager and ErrorDialogService
        builder.Services.AddTransient<IPushNotificationManager, PushNotificationManager>();
        builder.Services.AddTransient<ErrorDialogService>();

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
