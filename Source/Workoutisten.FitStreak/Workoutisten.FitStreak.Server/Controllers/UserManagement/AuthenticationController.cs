using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Authentication;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
using User = Workoutisten.FitStreak.Server.Model.Account.User;
using UserDto = Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person.User;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private IAuthenticationService AuthenticationService { get; }

    private IConverterWrapper Converter { get; }

    public AuthenticationController(IAuthenticationService authenticationService, IConverterWrapper converter)
    {
        AuthenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        Converter = converter ?? throw new ArgumentNullException(nameof(converter));
    }

    [HttpPost]
    [Route("login", Name = nameof(Login))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        if (authenticationRequest?.Email is null || authenticationRequest.Password is null) return BadRequest();

        var loginResult = await AuthenticationService.LoginAsync(authenticationRequest.Email, authenticationRequest.Password);

        return loginResult.Status switch
        {
            ResultStatus.BadRequest => BadRequest(),
            ResultStatus.Unauthorized => Unauthorized(),
            ResultStatus.ServerError => Problem(statusCode: StatusCodes.Status503ServiceUnavailable),
            ResultStatus.Successful => Ok(new AuthenticationResponse
            {
                RefreshToken = loginResult.RefreshToken,
                Jwt = loginResult.Jwt,
                User = await Converter.ToDto<User, UserDto>(loginResult.User)
            }),
            _ => throw new ArgumentOutOfRangeException(nameof(loginResult.Status), $"Not expected login status value: {loginResult.Status}")
        };
    }
}