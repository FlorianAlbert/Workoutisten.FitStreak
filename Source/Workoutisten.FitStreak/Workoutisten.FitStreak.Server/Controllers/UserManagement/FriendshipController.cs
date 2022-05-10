using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/friendship")]
public class FriendshipController : ControllerBase
{
    [HttpPost]
    [Route("request")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
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

    [HttpDelete]
    [Route("friend")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteFriendship([FromBody] string friendsEmail)
    {
        return BadRequest();
    }
}
