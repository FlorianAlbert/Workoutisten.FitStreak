using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IPasswordService
{
    Task<Result<bool>> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);

    Task<Result<bool>> RequestPasswordResetAsync(string email);

    Task<Result<bool>> ConfirmPasswordResetAsync(Guid passwordForgottenKey, string newPassword);
}
