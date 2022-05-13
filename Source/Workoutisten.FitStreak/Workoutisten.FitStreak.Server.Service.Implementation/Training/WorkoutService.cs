using Workoutisten.FitStreak.Server.Model.Workout;
using Workoutisten.FitStreak.Server.Service.Interface.Training;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Training;

public class WorkoutService : IWorkoutService
{
    public async Task<Workout> CreateOrUpdateAsync(Workout entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Workout entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Workout>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Workout>> GetAllForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Workout> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
