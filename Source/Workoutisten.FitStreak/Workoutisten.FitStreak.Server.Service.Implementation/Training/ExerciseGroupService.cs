using Workoutisten.FitStreak.Server.Model.Workout;
using Workoutisten.FitStreak.Server.Service.Interface.Training;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Training;

public class ExerciseGroupService : IExerciseGroupService
{
    public async Task<WorkoutEntry> CreateOrUpdateAsync(WorkoutEntry entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(WorkoutEntry entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<WorkoutEntry>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<WorkoutEntry>> GetAllForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<WorkoutEntry> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
