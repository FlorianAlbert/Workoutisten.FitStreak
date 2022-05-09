using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Authentication;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[Route("api/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequestDto authenticationRequest)
    {
        throw new NotImplementedException();
    }
}