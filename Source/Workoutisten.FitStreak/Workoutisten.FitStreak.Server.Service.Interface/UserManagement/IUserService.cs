using Workoutisten.FitStreak.Server.Model.Account;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IUserService
{
    Task<Result> DeleteUserAsync(Guid ownUserId, Guid userToDeleteId);

    Task<Result<User>> GetUserAsync(Guid userId);


    Task<Result<User>> UpdateUserAsync(Guid userId, string? email = null, string? firstName = null, string? lastName = null);
}