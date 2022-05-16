using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workoutisten.FitStreak.Client.RestClient;
using Workoutisten.FitStreak.Data.Models;
using Workoutisten.FitStreak.Data.Models.User;

namespace Workoutisten.FitStreak.Converter.User
{
    public class RegisterConverter : IConverter<RegisterModel, RegistrationRequest>
    {
        public Task<RegistrationRequest> ToDto(RegisterModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new RegistrationRequest
            {
                Email = entity.Email,
                Password = entity.Password,
                FirstName = entity.FirstName,
                LastName = entity.LastName
            };

            return Task.FromResult(dto);
        }

        public Task<RegisterModel> ToEntity(RegistrationRequest dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            var entity = new RegisterModel
            {
                Email = dto.Email,
                Password =dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            return Task.FromResult(entity);
        }
    }
}
