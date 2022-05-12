using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IUserService : IBaseEntityService<User>
{
    Task<int> HasDoneExerciseAsync(Guid userId);
}