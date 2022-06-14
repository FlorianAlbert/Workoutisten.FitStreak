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
    internal class ExerciseGroupConverter : IConverter<ExerciseGroupModel, ExerciseGroup>
    {
        public Task<ExerciseGroup> ToDto(ExerciseGroupModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new ExerciseGroup
            {
                CreatedAt = entity.CreatedAt,
                DoneExerciseIds = entity.DoneExercises.Select(ex => ex.ExerciseId).ToList(),
                ExerciseGroupId = entity.ExerciseGroupId,
                GroupName = entity.GroupName,
                ParticipantIds = entity.ParticipantIds,
                WorkoutId = entity.WorkoutId
            };

            return Task.FromResult(dto);
        }

        public Task<ExerciseGroupModel> ToEntity(ExerciseGroup dto)
        {
            if (dto is null)
            {
                throw new ArgumentOutOfRangeException(nameof(dto));
            }

            var entity = new ExerciseGroupModel()
            {
                WorkoutId = dto.WorkoutId,
                ParticipantIds = dto.ParticipantIds.ToList(),
                GroupName = dto.GroupName,
                ExerciseGroupId= dto.ExerciseGroupId,
                CreatedAt = dto.CreatedAt
            };

            return Task.FromResult(entity);
        }
    }
}
