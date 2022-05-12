namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IPasswordService
{
    Task<bool> ChangePasswordAsync(Guid userId, string oldPassword, string newPassword);

    Task<bool> RequestPasswordResetAsync(string email);

    Task<bool> ConfirmPasswordResetAsync(Guid passwordForgottenKey, string newPassword);
}
