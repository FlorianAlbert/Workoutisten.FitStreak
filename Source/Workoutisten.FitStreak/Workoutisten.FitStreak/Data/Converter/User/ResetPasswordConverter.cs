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
    internal class ResetPasswordConverter : IConverter<ResetPasswordModel, ResetPassword>
    {
        public Task<ResetPassword> ToDto(ResetPasswordModel entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new ResetPassword()
            {
                Email = entity.Email,
                NewPassword = entity.NewPassword,
                PasswordForgottenKey = entity.VerificationCode
            };

            return Task.FromResult(dto);
        }

        public Task<ResetPasswordModel> ToEntity(ResetPassword dto)
        {
            if (dto is null)
            {
                throw new ArgumentOutOfRangeException(nameof(dto));
            }

            var entity = new ResetPasswordModel()
            {
                Email = dto.Email,
                NewPassword = dto.NewPassword,
                VerificationCode = dto.PasswordForgottenKey
            };

            return Task.FromResult(entity);
        }
    }
}
