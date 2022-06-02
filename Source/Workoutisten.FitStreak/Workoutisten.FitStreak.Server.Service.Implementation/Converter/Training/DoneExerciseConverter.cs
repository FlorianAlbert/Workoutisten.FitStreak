using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using DoneExerciseEntity = Workoutisten.FitStreak.Server.Model.Excercise.DoneExercise;
using DoneExerciseDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.DoneExercise;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Converter.Training;

public class DoneExerciseConverter : IConverter<DoneExerciseEntity, DoneExerciseDto>
{
    public Task<DoneExerciseDto> ToDto(DoneExerciseEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        return Task.FromResult(new DoneExerciseDto
        {
            CreatedAt = entity.CreatedAt,
            ExerciseId = entity.Id,
            DoneExerciseId = entity.Id,
            ExerciseGroupId = entity.ExerciseGroup?.Id,
            SetIds = entity.Sets.Select(x => x.Id)
        });
    }

    public Task<DoneExerciseEntity> ToEntity(DoneExerciseDto dto)
    {
        throw new NotImplementedException();
    }
}
