using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.Training;
public interface IExerciseService
{
    Task<Result<Exercise>> CreateExercise(Guid userId, string name, string description, ExerciseCategory exerciseCategory);

    Task<Result> DeleteExercise(Guid userId, Guid exerciseId);

    Task<Result<Exercise>> GetExercise(Guid userId, Guid exerciseId);

    Task<Result<IEnumerable<Exercise>>> GetExercises(Guid userId);

    Task<Result<Exercise>> UpdateExercise(Guid userId, Guid exerciseId, string? name, string? description, ExerciseCategory exerciseCategory);
}
