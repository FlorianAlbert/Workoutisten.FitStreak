using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Authentication;
using Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Password;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[Route("api/password")]
[ApiController]
public class PasswordController : ControllerBase
{
    [HttpPost]
    [Route("change")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequest)
    {
        //Unauthorized if email is inappropriate to usercontext
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("requestReset")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequestDto resetPasswordRequest)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Route("reset")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
    {
        throw new NotImplementedException();
    }
}