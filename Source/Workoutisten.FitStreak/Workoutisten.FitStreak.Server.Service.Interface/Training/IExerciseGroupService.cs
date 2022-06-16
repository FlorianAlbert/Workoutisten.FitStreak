using Workoutisten.FitStreak.Server.Model.Workout;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.Training;
public interface IExerciseGroupService
{
    Task<Result<ExerciseGroup>> CreateNewExerciseGroup(Guid userId, string name, Guid? workoutId = null);

    Task<Result> DeleteExerciseGroup(Guid userId, Guid exerciseGroupId);

    Task<Result<IEnumerable<ExerciseGroup>>> GetAllExerciseGroupsForUser(Guid userId);

    Task<Result<ExerciseGroup>> GetExerciseGroupForUser(Guid userId, Guid exerciseGroupId);
}
