using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Database.Interface;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Password;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/password")]
public class PasswordController : ControllerBase
{
    private IPasswordService PasswordService { get; }

    private IRepository Repository { get; }

    public PasswordController(IPasswordService passwordService, IRepository repository)
    {
        PasswordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
        Repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpPost]
    [Route("change", Name = nameof(ChangePassword))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        if (string.IsNullOrEmpty(changePasswordRequest?.Email) ||
           string.IsNullOrEmpty(changePasswordRequest.OldPassword) ||
           string.IsNullOrEmpty(changePasswordRequest.NewPassword))
            return BadRequest("One or more of the following values were empty: email, oldPassword, newPassword !");

        var userId = await User.GetUserIdAsync();
        if (userId is null) return Unauthorized("There was no sufficient userId in the JWT!");
        
        var result = await PasswordService.ChangePasswordAsync(userId.Value,
                                                               changePasswordRequest.Email,
                                                               changePasswordRequest.OldPassword,
                                                               changePasswordRequest.NewPassword);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpPost]
    [Route("requestReset", Name = nameof(RequestPasswordReset))]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> RequestPasswordReset([FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        if (string.IsNullOrEmpty(resetPasswordRequest?.Email)) return BadRequest("The value of the send email was empty!");

        var result = await PasswordService.RequestPasswordResetAsync(resetPasswordRequest.Email);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpPost]
    [Route("reset", Name = nameof(ResetPassword))]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
    {
        if (string.IsNullOrEmpty(resetPassword?.Email) ||
           string.IsNullOrEmpty(resetPassword.PasswordForgottenKey) ||
           string.IsNullOrEmpty(resetPassword.NewPassword))
            return BadRequest("One or more of the following values were empty: email, passwordForgottenKey, newPassword!");

        var result = await PasswordService.ResetPasswordAsync(resetPassword.PasswordForgottenKey,
                                                              resetPassword.Email,
                                                              resetPassword.NewPassword);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }
}