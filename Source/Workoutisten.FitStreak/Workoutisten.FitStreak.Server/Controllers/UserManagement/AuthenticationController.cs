using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Authentication;
using Workoutisten.FitStreak.Server.DataTransferObjects.UserManagement.Person;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private IAuthenticationService AuthenticationService { get; }

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        AuthenticationService = authenticationService;
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        if (authenticationRequest?.Email is null || authenticationRequest?.Password is null) return BadRequest();

        var loginResult = await AuthenticationService.LoginAsync(authenticationRequest.Email, authenticationRequest.Password);

        switch (loginResult.Status)
        {
            case LoginResultStatus.BadRequest: return BadRequest();
            case LoginResultStatus.Unauthorized: return Unauthorized();
            case LoginResultStatus.Successful: return Ok(new AuthenticationResponse
            {
                Token = loginResult.Token,
                //ToDo Converter benutzen
                User = new User()
            });
            default: throw new ArgumentOutOfRangeException(nameof(loginResult.Status), $"Not expected login status value: {loginResult.Status}");
        }
    }
}