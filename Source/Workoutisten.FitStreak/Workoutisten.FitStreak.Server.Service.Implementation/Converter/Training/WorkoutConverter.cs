using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using WorkoutEntity = Workoutisten.FitStreak.Server.Model.Workout.Workout;
using WorkoutDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.Workout.Workout;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Converter.Training;

public class WorkoutConverter : IConverter<WorkoutEntity, WorkoutDto>
{
    public Task<WorkoutDto> ToDto(WorkoutEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var dto = new WorkoutDto
        {
            CreatedAt = entity.CreatedAt,
            WorkoutName = entity.Name,
            Description = entity.Description,
            WorkoutId = entity.Id,
            DoneExerciseIds = entity.ExerciseGroups.Select(wee => wee.Id).ToArray(),
            ExerciseIds = entity.WorkoutExercises.Select(we => we.ExerciseId).ToArray()
        };
        return Task.FromResult(dto);
    }

    public Task<WorkoutEntity> ToEntity(WorkoutDto dto)
    {
        throw new NotImplementedException();
    }
}
