namespace Workoutisten.FitStreak.Server.Service.Implementation.Extension;

public static class StringExtension
{
    public static string NormalizeEmail(this string email) => email.ToLower();
}
