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
    internal class LoginConverter : IConverter<LoginModel, AuthenticationRequest>
    {
        public Task<AuthenticationRequest> ToDto(LoginModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new AuthenticationRequest()
            {
                Email = entity.Email,
                Password = entity.Password
            };

            return Task.FromResult(dto);
        }

        public Task<LoginModel> ToEntity(AuthenticationRequest dto)
        {
            if(dto is null)
            {
                throw new ArgumentOutOfRangeException(nameof(dto));
            }

            var entity = new LoginModel()
            {
                Email = dto.Email,
                Password = dto.Password
            };

            return Task.FromResult(entity);
        }
    }
}
