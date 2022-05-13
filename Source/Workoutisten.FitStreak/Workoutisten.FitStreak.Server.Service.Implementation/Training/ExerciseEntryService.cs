using Workoutisten.FitStreak.Server.Model.Excercise;
using Workoutisten.FitStreak.Server.Service.Interface;
using Workoutisten.FitStreak.Server.Service.Interface.Training;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Training;
public class ExerciseEntryService : IExerciseEntryService
{
    public async Task<ExerciseEntry> CreateOrUpdateAsync(ExerciseEntry entity) => entity switch
        {
            CardioExerciseEntry cardioExerciseEntry => await CreateOrUpdateAsync(cardioExerciseEntry),
            StrengthExerciseEntry strengthExerciseEntry => await CreateOrUpdateAsync(strengthExerciseEntry),
            _ => throw new ArgumentOutOfRangeException(nameof(entity), $"Not expected enitiy value: {entity}")
        };

    public async Task<CardioExerciseEntry> CreateOrUpdateAsync(CardioExerciseEntry entity)
    {
        throw new NotImplementedException();
    }

    public async Task<StrengthExerciseEntry> CreateOrUpdateAsync(StrengthExerciseEntry entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(ExerciseEntry entity) => entity switch
    {
        CardioExerciseEntry cardioExerciseEntry => await DeleteAsync(cardioExerciseEntry),
        StrengthExerciseEntry strengthExerciseEntry => await DeleteAsync(strengthExerciseEntry),
        _ => throw new ArgumentOutOfRangeException(nameof(entity), $"Not expected enitiy value: {entity}")
    };

    public async Task<bool> DeleteAsync(CardioExerciseEntry entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(StrengthExerciseEntry entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ExerciseEntry>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<CardioExerciseEntry>> IBaseEntityService<CardioExerciseEntry>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<StrengthExerciseEntry>> IBaseEntityService<StrengthExerciseEntry>.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ExerciseEntry>> GetAllForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<CardioExerciseEntry>> IBaseEntityService<CardioExerciseEntry>.GetAllForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    async Task<IEnumerable<StrengthExerciseEntry>> IBaseEntityService<StrengthExerciseEntry>.GetAllForUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ExerciseEntry> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    async Task<CardioExerciseEntry> IBaseEntityService<CardioExerciseEntry>.GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    async Task<StrengthExerciseEntry> IBaseEntityService<StrengthExerciseEntry>.GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
