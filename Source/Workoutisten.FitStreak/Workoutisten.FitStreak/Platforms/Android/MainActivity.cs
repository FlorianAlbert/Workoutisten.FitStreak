using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.LocalNotification;

namespace Workoutisten.FitStreak;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        NotificationCenter.CreateNotificationChannel();
        NotificationCenter.NotifyNotificationTapped(Intent);

        base.OnCreate(savedInstanceState);
    }
    protected override void OnNewIntent(Intent intent)
    {
        NotificationCenter.NotifyNotificationTapped(intent);
        base.OnNewIntent(intent);
    }
}
