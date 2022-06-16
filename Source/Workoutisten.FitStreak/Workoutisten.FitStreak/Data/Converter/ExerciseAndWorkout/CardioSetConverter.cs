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
    public class CardioSetConverter : IConverter<CardioExerciseSetModel, CardioSet>
    {
        public Task<CardioSet> ToDto(CardioExerciseSetModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new CardioSet()
            {
                DoneExerciseId = entity.DoneExerciseId,
                Id = entity.SetId,
                Distance = entity.Distance ?? 0,
                Ticks = entity.Duration.HasValue? entity.Duration.Value.Ticks : 0
            };

            return Task.FromResult(dto);
        }

        public Task<CardioExerciseSetModel> ToEntity(CardioSet dto)
        {
            if (dto is null)
            {
                throw new ArgumentOutOfRangeException(nameof(dto));
            }

            var entity = new CardioExerciseSetModel()
            {
                Duration = new TimeSpan(dto.Ticks),
                Distance = dto.Distance,
                SetId = dto.Id,
                DoneExerciseId = dto.DoneExerciseId,
                //SetNumber??
            };

            return Task.FromResult(entity);
        }
    }
}
