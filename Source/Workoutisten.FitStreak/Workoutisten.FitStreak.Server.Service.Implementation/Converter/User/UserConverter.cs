using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using UserEntity = Workoutisten.FitStreak.Server.Model.Account.User;
using UserDto = Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person.User;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Converter.User
{
    public class UserConverter : IConverter<UserEntity, UserDto>
    {
        public Task<UserDto> ToDto(UserEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var dto = new UserDto
            {
                UserId = entity.Id,
                Email = entity.NormalizedEmail,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                CreatedDate = entity.CreatedAt
            };

            return Task.FromResult(dto);
        }

        public Task<UserEntity> ToEntity(UserDto dto)
        {
            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }
            var entity = new UserEntity
            {
                Id = dto.UserId,
                NormalizedEmail = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                CreatedAt = dto.CreatedDate
            };

            return Task.FromResult(entity);
        }
    }
}
