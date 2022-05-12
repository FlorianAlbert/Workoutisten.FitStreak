using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/registration")]
public class RegistrationController : ControllerBase
{
    [HttpPost]
    [Route("request")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestRegistration()
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("confirm/{passwordForgottenKey}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmRegistration(Guid passwordForgottenKey)
    {
        return BadRequest();
    }
}