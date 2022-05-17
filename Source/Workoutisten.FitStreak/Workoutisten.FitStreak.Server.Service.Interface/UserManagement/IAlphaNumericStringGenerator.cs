namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

public interface IAlphaNumericStringGenerator
{
    Task<string> GenerateAlphaNumericString();
}
