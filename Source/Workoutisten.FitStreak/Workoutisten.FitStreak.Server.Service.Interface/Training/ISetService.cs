using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Service.Interface.Data;

namespace Workoutisten.FitStreak.Server.Service.Interface.Training
{
    public interface ISetService
    {
        Task<Result<StrengthSet>> CreateStrengthSet(Guid userId, Guid doneExerciseId, double weight, int repetitions);

        Task<Result<CardioSet>> CreateCardioSet(Guid userId, Guid doneExerciseId, double distance, TimeSpan duration);

        Task<Result<StrengthSet>> UpdateStrengthSet(Guid userId, Guid setId, double weight, int repetitions);

        Task<Result<CardioSet>> UpdateCardioSet(Guid userId, Guid setId, double distance, TimeSpan duration);

        Task<Result> DeleteSet(Guid userId, Guid setId);

        Task<Result<IEnumerable<Set>>> GetAllSetsForUser(Guid userId);

        Task<Result<Set>> GetSetForUser(Guid userId, Guid setId);
    }
}
