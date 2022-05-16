using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Authentication;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Password;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/password")]
public class PasswordController : ControllerBase
{
    [HttpPost]
    [Route("change", Name = nameof(ChangePassword))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        //Unauthorized if email is inappropriate to usercontext
        return BadRequest();
    }

    [HttpPost]
    [Route("requestReset", Name = nameof(RequestPasswordReset))]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("reset", Name = nameof(ResetPassword))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
    {
        return BadRequest();
    }
}