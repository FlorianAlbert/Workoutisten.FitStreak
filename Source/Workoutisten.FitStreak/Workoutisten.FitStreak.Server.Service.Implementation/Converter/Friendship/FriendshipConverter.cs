using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using FriendshipRequestEntity = Workoutisten.FitStreak.Server.Model.Account.FriendshipRequest;
using FriendshipRequestDto = Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Friendship.FriendshipRequest;
using UserEntity = Workoutisten.FitStreak.Server.Model.Account.User;
using UserDto = Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person.User;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Converter.Friendship;

public class FriendshipConverter : IConverter<FriendshipRequestEntity, FriendshipRequestDto>
{
    private IConverterWrapper Converter { get; }

    public FriendshipConverter(IConverterWrapper converter)
    {
        Converter = converter ?? throw new ArgumentNullException(nameof(converter));
    }

    public async Task<FriendshipRequestDto> ToDto(FriendshipRequestEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        var dto = new FriendshipRequestDto
        {
            FriendshipRequestId = entity.Id,
            RequestedUser = await Converter.ToDto<UserEntity, UserDto>(entity.RequestedUser),
            RequestingUser = await Converter.ToDto<UserEntity, UserDto>(entity.RequestingUser)
        };
        return dto;
    }

    public async Task<FriendshipRequestEntity> ToEntity(FriendshipRequestDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var entity = new FriendshipRequestEntity
        {
            Id = dto.FriendshipRequestId,
            RequestedUser = await Converter.ToEntity<UserDto, UserEntity>(dto.RequestedUser),
            RequestingUser = await Converter.ToEntity<UserDto, UserEntity>(dto.RequestingUser)
        };
        return entity;
    }
}
