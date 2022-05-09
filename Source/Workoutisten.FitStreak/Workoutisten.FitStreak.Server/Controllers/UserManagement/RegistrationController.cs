using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Authentication;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[Route("api/registration")]
[ApiController]
public class RegistrationController : ControllerBase
{
    [HttpPost]
    [Route("request")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestRegistration()
    {
        return BadRequest();
    }
}