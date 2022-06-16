using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Converter;
using Workoutisten.FitStreak.Data.Models.User;

namespace Workoutisten.FitStreak.Data.Converter.User
{
    public class UserConverter : IConverter<UserModel, Client.RestClient.User>
    {

        public Task<Client.RestClient.User> ToDto(UserModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new Client.RestClient.User
            {
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                ExerciseStreak = entity.Streak,
                LastExercise = entity.LastExercise,
                UserId = entity.UserId,
                //StreakRecord = dto.Maxstreak
            };

            return Task.FromResult(dto);
        }

        public Task<UserModel> ToEntity(Client.RestClient.User dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = new UserModel  
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                LastExercise = dto.LastExercise,
                UserId = dto.UserId,
                Streak = dto.ExerciseStreak,
                //StreakRecord = dto.Maxstreak
            };

            return Task.FromResult(entity);
        }
    }
}
