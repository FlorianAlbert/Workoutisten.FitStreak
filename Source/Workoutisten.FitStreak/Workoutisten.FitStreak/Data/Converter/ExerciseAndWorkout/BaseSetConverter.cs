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
    public class BaseSetConverter : IConverter<BaseExerciseSetModel, Set>
    {
        public Task<Set> ToDto(BaseExerciseSetModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Set dto = null;

            switch (entity)
            {
                case CardioExerciseSetModel cardioExerciseSetModel:
                    dto = new CardioSet()
                    {
                        DoneExerciseId = cardioExerciseSetModel.DoneExerciseId,
                        Id = cardioExerciseSetModel.SetId,
                        Distance = cardioExerciseSetModel.Distance ?? 0,
                        Ticks = cardioExerciseSetModel.Duration.HasValue ? cardioExerciseSetModel.Duration.Value.Ticks : 0
                    };
                    break;
                case StrengthExerciseSetModel strengthExerciseSetModel:
                    dto = new StrengthSet()
                    {
                        DoneExerciseId = strengthExerciseSetModel.DoneExerciseId,
                        Id = strengthExerciseSetModel.SetId,
                        Repetitions = strengthExerciseSetModel.Reps ?? 0,
                        Weight = strengthExerciseSetModel.Weight ?? 0
                    };
                    break;
                default:
                    throw new Exception(); // TODO: Besseres Exception handling

            }

            return Task.FromResult(dto);
        }

        public Task<BaseExerciseSetModel> ToEntity(Set dto)
        {
            if (dto is null)
            {
                throw new ArgumentOutOfRangeException(nameof(dto));
            }

            BaseExerciseSetModel entity = null;

            switch (dto)
            {
                case StrengthSet strengthSet:
                    entity = new StrengthExerciseSetModel()
                    {
                        Weight = strengthSet.Weight,
                        Reps = strengthSet.Repetitions,
                        SetId = strengthSet.Id,
                        DoneExerciseId = strengthSet.DoneExerciseId,
                        //SetNumber??
                    };
                    break;
                case CardioSet cardioSet:
                    entity = new CardioExerciseSetModel()
                    {
                        Duration = new TimeSpan(cardioSet.Ticks),
                        Distance = cardioSet.Distance,
                        SetId = cardioSet.Id,
                        DoneExerciseId = cardioSet.DoneExerciseId,
                        //SetNumber??
                    };
                    break;
                default:
                    throw new Exception(); // TODO: besseres Error Handling
            }

            return Task.FromResult(entity);
        }
    }
}
