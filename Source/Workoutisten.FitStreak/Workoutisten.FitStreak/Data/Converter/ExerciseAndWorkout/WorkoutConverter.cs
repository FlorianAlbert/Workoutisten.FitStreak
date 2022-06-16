using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Converter;
using Workoutisten.FitStreak.Data.Models.Workout;

namespace Workoutisten.FitStreak.Data.Converter.ExerciseAndWorkout
{
    internal class WorkoutConverter : IConverter<WorkoutModel, Workout>
    {

        public Task<Workout> ToDto(WorkoutModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new Workout()
            {
                WorkoutName = entity.WorkoutName,
                WorkoutId = entity.Guid,
                CreatedAt = entity.CreatedAt,
                Description = entity.Description,
                ExerciseIds = entity.Exercises
            };

            return Task.FromResult(dto);
        }

        public Task<WorkoutModel> ToEntity(Workout dto)
        {
            if (dto is null)
            {
                throw new ArgumentOutOfRangeException(nameof(dto));
            }

            var entity = new WorkoutModel()
            {
                Guid = dto.WorkoutId.HasValue? dto.WorkoutId.Value : Guid.Empty,
                WorkoutName = dto.WorkoutName,
                CreatedAt = dto.CreatedAt,
                Exercises = dto.ExerciseIds.ToList(),
                Description = dto.Description,

            };

            return Task.FromResult(entity);
        }
    }
}
