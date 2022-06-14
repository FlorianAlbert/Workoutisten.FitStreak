using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Converter;
using Workoutisten.FitStreak.Data.Models.Workout;

namespace Workoutisten.FitStreak.Data.Converter.ExerciseAndWorkout
{
    internal class DoneExerciseConverter : IConverter<DoneExerciseModel, DoneExercise>
    {
        public Task<DoneExercise> ToDto(DoneExerciseModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new DoneExercise()
            {
                DoneExerciseId = entity.DoneExerciseId,
                CreatedAt = entity.CreatedAt,
                ExerciseGroupId = entity.ExerciseGroupId,
                SetIds = entity.SetIds,
                ExerciseId = entity.ExerciseId,
            };

            return Task.FromResult(dto);
        }

        public Task<DoneExerciseModel> ToEntity(DoneExercise dto)
        {
            if (dto is null)
            {
                throw new ArgumentOutOfRangeException(nameof(dto));
            }

            var entity = new DoneExerciseModel()
            {
                SetIds = dto.SetIds,
                CreatedAt = dto.CreatedAt,
                DoneExerciseId = dto.DoneExerciseId,
                ExerciseGroupId = dto.ExerciseGroupId,
                ExerciseId = dto.ExerciseId,
            };

            return Task.FromResult(entity);
        }
    }
}
