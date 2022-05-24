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
                //ExerciseIds = entity.Exercises
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
                Guid = dto.WorkoutId,
                //Exercises = dto.ExerciseIds.ToList(),
                //Description = dto.Description
                //LastTraining = dto.LastTraining
                WorkoutName = dto.WorkoutName

            };

            return Task.FromResult(entity);
        }
    }
}
