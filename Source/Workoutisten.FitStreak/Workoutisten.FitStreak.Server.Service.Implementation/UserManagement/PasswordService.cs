using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class PasswordService : IPasswordService
{
    public async Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ConfirmPasswordResetAsync(Guid passwordForgottenKey, string newPassword)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RequestPasswordResetAsync(string email)
    {
        throw new NotImplementedException();
    }
}
