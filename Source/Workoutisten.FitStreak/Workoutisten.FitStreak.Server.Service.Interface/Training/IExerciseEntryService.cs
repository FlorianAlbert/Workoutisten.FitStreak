using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Service.Interface.Training;
public interface IExerciseEntryService : IBaseEntityService<ExerciseEntry>, 
    IBaseEntityService<CardioExerciseEntry>, 
    IBaseEntityService<StrengthExerciseEntry>
{
}
