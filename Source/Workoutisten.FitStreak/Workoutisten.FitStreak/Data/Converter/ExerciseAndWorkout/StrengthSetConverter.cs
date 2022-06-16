using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Converter;
using Workoutisten.FitStreak.Data.Models.Workout;

namespace Workoutisten.FitStreak.Data.Converter.ExerciseAndWorkout
{
    public class StrengthSetConverter : IConverter<StrengthExerciseSetModel, StrengthSet>
    {
        public Task<StrengthSet> ToDto(StrengthExerciseSetModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new StrengthSet()
            {
                DoneExerciseId = entity.DoneExerciseId,
                Id = entity.SetId,
                Repetitions = entity.Reps?? 0,
                Weight = entity.Weight?? 0
            };

            return Task.FromResult(dto);
        }

        public Task<StrengthExerciseSetModel> ToEntity(StrengthSet dto)
        {
            if (dto is null)
            {
                throw new ArgumentOutOfRangeException(nameof(dto));
            }

            var entity = new StrengthExerciseSetModel()
            {
                Weight = dto.Weight,
                Reps = dto.Repetitions,
                SetId = dto.Id,
                DoneExerciseId = dto.DoneExerciseId,
                //SetNumber??
            };

            return Task.FromResult(entity);
        }
    }
}
