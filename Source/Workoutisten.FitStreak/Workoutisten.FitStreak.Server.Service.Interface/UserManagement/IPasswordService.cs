using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IPasswordService
{
    Task<Result> ChangePasswordAsync(Guid userId, string email, string oldPassword, string newPassword);

    Task<Result> RequestPasswordResetAsync(string email);

    Task<Result> ResetPasswordAsync(string passwordForgottenKey, string email, string newPassword);
}
