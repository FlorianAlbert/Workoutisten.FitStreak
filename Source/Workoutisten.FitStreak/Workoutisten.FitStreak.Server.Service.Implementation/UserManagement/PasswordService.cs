using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Service.Implementation.UserManagement;
public class PasswordService : IPasswordService
{
    public Task<Result> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<Result> ConfirmPasswordResetAsync(Guid passwordForgottenKey, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RequestPasswordResetAsync(string email)
    {
        throw new NotImplementedException();
    }
}
