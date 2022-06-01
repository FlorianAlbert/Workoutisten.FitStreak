using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.Training;
public interface IDoneExerciseService
{
    Task<Result<DoneExercise>> CreateDoneExercise(Guid userId, Guid exerciseId, Guid? exerciseGroupId = null);

    Task<Result> DeleteDoneExercise(Guid userId, Guid doneExerciseId);

    Task<Result<IEnumerable<DoneExercise>>> GetAllDoneExercisesForUser(Guid userId);

    Task<Result<DoneExercise>> GetDoneExerciseForUser(Guid userId, Guid doneExerciseId);
}
