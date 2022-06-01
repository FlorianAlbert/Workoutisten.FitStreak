using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using ExerciseGroupEntity = Workoutisten.FitStreak.Server.Model.Workout.ExerciseGroup;
using ExerciseGroupDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.Group.ExerciseGroup;
using Workoutisten.FitStreak.Server.Service.Implementation.Extension;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Converter.Training;

public class ExerciseGroupConverter : IConverter<ExerciseGroupEntity, ExerciseGroupDto>
{
    public Task<ExerciseGroupDto> ToDto(ExerciseGroupEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        return Task.FromResult(new ExerciseGroupDto
        {
            CreatedAt = entity.CreatedAt,
            DoneExerciseIds = entity.DoneExercises.Select(x => x.Id).ToArray(),
            ExerciseGroupId = entity.Id,
            ParticipantIds = entity.Participants.Select(x => x.Id).ToArray(),
            WorkoutId = entity.Workout.Id
        });
    }

    public Task<ExerciseGroupEntity> ToEntity(ExerciseGroupDto dto)
    {
        throw new NotImplementedException();
    }
}
