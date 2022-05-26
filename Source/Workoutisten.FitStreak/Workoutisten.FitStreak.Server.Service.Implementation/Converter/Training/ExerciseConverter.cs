using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using ExerciseEntity = Workoutisten.FitStreak.Server.Model.Excercise.Exercise;
using ExerciseDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.Exercise.Exercise;
using Workoutisten.FitStreak.Server.Service.Implementation.Extension;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Converter.Training;

public class ExerciseConverter : IConverter<ExerciseEntity, ExerciseDto>
{
    public Task<ExerciseDto> ToDto(ExerciseEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        return Task.FromResult(new ExerciseDto
        {
            CreatedAt = entity.CreatedAt,
            Description = entity.Description,
            ExerciseCategory = entity.Category.ToDto(),
            Name = entity.Name,
            ExerciseId = entity.Id,
            DoneExerciseIds = entity.ExerciseEntries.Select(ee => ee.Id).ToArray(),
            WorkoutIds = entity.WorkoutExercises.Select(we => we.WorkoutId).ToArray(),
            CreatorId = entity.Creator.Id
        });
    }

    public Task<ExerciseEntity> ToEntity(ExerciseDto dto)
    {
        throw new NotImplementedException();
    }
}
