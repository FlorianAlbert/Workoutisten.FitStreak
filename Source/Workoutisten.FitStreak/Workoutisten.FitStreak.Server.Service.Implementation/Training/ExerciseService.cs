using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Service.Interface.Training;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Training;

public class ExerciseService : IExerciseService
{
    public async Task<Exercise> CreateOrUpdateAsync(Exercise entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Exercise entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Exercise>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Exercise>> GetAllForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Exercise> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
