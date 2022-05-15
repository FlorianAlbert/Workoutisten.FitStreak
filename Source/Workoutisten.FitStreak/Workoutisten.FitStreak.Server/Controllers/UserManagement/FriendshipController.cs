using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Friendship;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/friendship")]
public class FriendshipController : ControllerBase
{
    [HttpGet]
    [Route("request")]
    [Authorize]
    [ProducesResponseType(typeof(FriendshipRequest[]),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetFriendshipRequests()
    {
        
        return BadRequest();
    }

    [HttpPost]
    [Route("request")]
    [Authorize]
    [ProducesResponseType(typeof(FriendshipRequest), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateFriendshipRequest([FromBody] string requestedEmail)
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("request/{friendshipRequestId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AcceptFriendshipRequest([FromRoute] Guid friendshipRequestId)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("request/{friendshipRequestId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeclineFriendshipRequest([FromRoute] Guid friendshipRequestId)
    {
        return BadRequest();
    }

    [HttpGet]
    [Route("friend/{friendId}")]
    [Authorize]
    [ProducesResponseType(typeof(User) ,StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetFriend([FromRoute] Guid friendId)
    {
       return BadRequest();
    }

    [HttpGet]
    [Route("friend")]
    [Authorize]
    [ProducesResponseType(typeof(User[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetFriends()
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("friend")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteFriend([FromBody] string friendsEmail)
    {
        return BadRequest();
    }
}
