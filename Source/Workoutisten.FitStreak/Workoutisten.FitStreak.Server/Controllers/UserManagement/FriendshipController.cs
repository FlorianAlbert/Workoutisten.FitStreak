using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
using UserEntity = Workoutisten.FitStreak.Server.Model.Account.User;
using UserDto = Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person.User;
using FriendshipRequestEntity = Workoutisten.FitStreak.Server.Model.Account.FriendshipRequest;
using FriendshipRequestDto = Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Friendship.FriendshipRequest;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Friendship;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/friendship")]
public class FriendshipController : ControllerBase
{
    private IFriendshipService FriendshipService { get; }

    private IConverterWrapper Converter { get; }

    public FriendshipController(IFriendshipService friendshipService, IConverterWrapper converter)
    {
        FriendshipService = friendshipService ?? throw new ArgumentNullException(nameof(friendshipService));
        Converter = converter ?? throw new ArgumentNullException(nameof(converter));
    }

    [HttpPost]
    [Route("request/{friendshipRequestId}", Name = nameof(AcceptFriendshipRequest))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> AcceptFriendshipRequest([FromRoute] Guid friendshipRequestId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await FriendshipService.AcceptFriendshipRequestAsync(userId.Value, friendshipRequestId);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpPost]
    [Route("request", Name = nameof(CreateFriendshipRequest))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> CreateFriendshipRequest([FromBody] FriendRequest friendRequest)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");
        if (string.IsNullOrEmpty(friendRequest?.Email)) return BadRequest("The requestedUserEmail was null or empty!");

        var result = await FriendshipService.CreateFriendshipRequestAsync(userId.Value, friendRequest.Email);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpDelete]
    [Route("request/{friendshipRequestId}", Name = nameof(DeclineFriendshipRequest))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> DeclineFriendshipRequest([FromRoute] Guid friendshipRequestId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await FriendshipService.DeclineFriendshipRequestAsync(userId.Value, friendshipRequestId);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpDelete]
    [Route("friend/follower/{followerId}", Name = nameof(DeleteFollower))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> DeleteFollower([FromRoute] Guid followerId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await FriendshipService.DeleteFollowerAsync(userId.Value, followerId);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpGet]
    [Route("friend/followed", Name = nameof(GetFollowedUsers))]
    [Authorize]
    [ProducesResponseType(typeof(UserDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetFollowedUsers()
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await FriendshipService.GetFollowedUsersAsync(userId.Value);
        if (result.Successful)
        {
            var userDtos = result
                .Value
                ?.Select(async user => await Converter.ToDto<UserEntity, UserDto>(user))
                .Select(task => task.Result)
                .Where(result => result is not null)
                .ToArray();
            return Ok(userDtos);
        }
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpGet]
    [Route("friend/follower", Name = nameof(GetFollower))]
    [Authorize]
    [ProducesResponseType(typeof(UserDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetFollower()
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await FriendshipService.GetFollowerAsync(userId.Value);
        if (result.Successful)
        {
            var userDtos = result
                .Value
                ?.Select(async user => await Converter.ToDto<UserEntity, UserDto>(user))
                .Select(task => task.Result)
                .Where(result => result is not null)
                .ToArray();
            return Ok(userDtos);
        }
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpGet]
    [Route("request/incoming", Name = nameof(GetIncomingFriendshipRequests))]
    [Authorize]
    [ProducesResponseType(typeof(FriendshipRequestDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetIncomingFriendshipRequests()
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await FriendshipService.GetIncomingFriendshipRequestsAsync(userId.Value);
        if (result.Successful)
        {
            var friendshipRequestDtos = result
                .Value
                ?.Select(async request => await Converter.ToDto<FriendshipRequestEntity, FriendshipRequestDto>(request))
                .Select(task => task.Result)
                .Where(result => result is not null)
                .ToArray();
            return Ok(friendshipRequestDtos);
        }
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpGet]
    [Route("request/outgoing", Name = nameof(GetOutgoingFriendshipRequests))]
    [Authorize]
    [ProducesResponseType(typeof(FriendshipRequestDto[]),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetOutgoingFriendshipRequests()
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await FriendshipService.GetOutgoingFriendshipRequestsAsync(userId.Value);
        if (result.Successful)
        {
            var friendshipRequestDtos = result
                .Value
                ?.Select(async request => await Converter.ToDto<FriendshipRequestEntity, FriendshipRequestDto>(request))
                .Select(task => task.Result)
                .Where(result => result is not null)
                .ToArray();
            return Ok(friendshipRequestDtos);
        }
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpDelete]
    [Route("friend/followed/{followedUserId}", Name = nameof(UnfollowUser))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> UnfollowUser([FromRoute] Guid followedUserId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await FriendshipService.UnfollowUserAsync(userId.Value, followedUserId);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpDelete]
    [Route("request/withdraw/{friendshipRequestId}", Name = nameof(WithdrawFriendshipRequest))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> WithdrawFriendshipRequest([FromRoute] Guid friendshipRequestId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await FriendshipService.WithdrawFriendshipRequestAsync(userId.Value, friendshipRequestId);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }
}
