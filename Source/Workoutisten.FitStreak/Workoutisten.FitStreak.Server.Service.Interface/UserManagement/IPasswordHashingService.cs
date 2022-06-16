namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

public interface IPasswordHashingService
{
    Task<string> HashPasswordForStorageAsync(string password);

    Task<bool> VerifyPasswordAsync(string password, string expectedPasswordHash);
}
