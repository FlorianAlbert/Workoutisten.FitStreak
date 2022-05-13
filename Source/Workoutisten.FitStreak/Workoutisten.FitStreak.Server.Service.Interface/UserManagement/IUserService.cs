namespace Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
public interface IUserService
{
    Task<int> HasDoneExerciseAsync(Guid userId);
}