using Workoutisten.FitStreak.Server.Model.Workout;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.Training;
public interface IWorkoutService
{
    Task<Result<Workout>> CreateWorkout(Guid userId, string name, string description, IEnumerable<Guid> exerciseIds);

    Task<Result> DeleteWorkout(Guid userId, Guid workoutId);

    Task<Result<Workout>> GetWorkout(Guid userId, Guid workoutId);

    Task<Result<IEnumerable<Workout>>> GetWorkouts(Guid userId);

    Task<Result<Workout>> UpdateWorkout(Guid userId, Guid workoutId, string? name, string? descritpion, IEnumerable<Guid>? exerciseIds);
}
