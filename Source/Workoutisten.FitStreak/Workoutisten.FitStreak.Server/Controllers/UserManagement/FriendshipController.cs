using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Friendship;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/friendship")]
public class FriendshipController : ControllerBase
{
    private IFriendshipService FriendshipService { get; }

    public FriendshipController(IFriendshipService friendshipService)
    {
        FriendshipService = friendshipService ?? throw new ArgumentNullException(nameof(friendshipService));
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
    public async Task<IActionResult> CreateFriendshipRequest([FromBody] string requestedUserEmail)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");
        if (string.IsNullOrEmpty(requestedUserEmail)) return BadRequest("The requestedUserEmail was null or empty!");

        var result = await FriendshipService.CreateFriendshipRequestAsync(userId.Value, requestedUserEmail);
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
    [Route("friend/followed/{followedUserId}", Name = nameof(DeleteFollower))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteFollower([FromRoute] Guid followedUserId)
    {
        return BadRequest();
    }

    [HttpGet]
    [Route("firend/followed", Name = nameof(GetFollowedUsers))]
    [Authorize]
    [ProducesResponseType(typeof(User[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetFollowedUsers()
    {
        return BadRequest();
    }

    [HttpGet]
    [Route("firend/follower", Name = nameof(GetFollower))]
    [Authorize]
    [ProducesResponseType(typeof(User[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetFollower()
    {
        return BadRequest();
    }

    [HttpGet]
    [Route("request/incoming", Name = nameof(GetIncomingFriendshipRequests))]
    [Authorize]
    [ProducesResponseType(typeof(FriendshipRequest[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetIncomingFriendshipRequests()
    {

        return BadRequest();
    }

    [HttpGet]
    [Route("request/outgoing", Name = nameof(GetOutgoingFriendshipRequests))]
    [Authorize]
    [ProducesResponseType(typeof(FriendshipRequest[]),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOutgoingFriendshipRequests()
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("friend/follower/{followedUserId}", Name = nameof(UnfollowUser))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UnfollowUser([FromRoute] Guid followedUserId)
    {
        return BadRequest();
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
