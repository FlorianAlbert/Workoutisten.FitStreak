using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Converter;
using Workoutisten.FitStreak.Data.Models.Workout;

namespace Workoutisten.FitStreak.Data.Converter.ExerciseAndWorkout
{
    internal class ExerciseConverter : IConverter<ExerciseModel, Exercise>
    {
        public Task<Exercise> ToDto(ExerciseModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new Exercise()
            {
                Description = entity.Description,
                Name = entity.Name,
                ExerciseId = entity.Guid,
                ExerciseCategory = (ExerciseCategory)entity.Category
            };

            return Task.FromResult(dto);
        }

        public Task<ExerciseModel> ToEntity(Exercise dto)
        {
            if (dto is null)
            {
                throw new ArgumentOutOfRangeException(nameof(dto));
            }

            var entity = new ExerciseModel()
            {
               Category = (Enums.ExerciseCategoryEnum)dto.ExerciseCategory,
               Guid = dto.ExerciseId,
               Name = dto.Name,
               Description = dto.Description
            };

            return Task.FromResult(entity);
        }
    }
}
