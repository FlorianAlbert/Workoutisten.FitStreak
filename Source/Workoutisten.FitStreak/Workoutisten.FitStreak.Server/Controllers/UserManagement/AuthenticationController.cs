using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Authentication;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
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
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest authenticationRequest)
    {
        if (authenticationRequest?.Email is null ||
            authenticationRequest.Password is null)
            return BadRequest("One or more of the following values were empty: email, password!");

        var result = await AuthenticationService.LoginAsync(authenticationRequest.Email, authenticationRequest.Password);
        if (result.Unsccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);
        else return Ok(new AuthenticationResponse
        {
            RefreshToken = result.Value.Tokens.RefreshToken,
            Jwt = result.Value.Tokens.Jwt,
            User = await Converter.ToDto<User, UserDto>(result.Value.User)
        });
    }

    [HttpPost]
    [Route("tokens", Name = nameof(RefreshTokens))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(TokenRefreshResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> RefreshTokens([FromBody] TokenRefreshRequest tokenRefreshRequest)
    {
        if (string.IsNullOrEmpty(tokenRefreshRequest?.RefreshToken) ||
            string.IsNullOrEmpty(tokenRefreshRequest.ExpiredJwt))
            return BadRequest("One or more of the following values were empty: refreshToken, expiredJwt!");

        var result = await AuthenticationService.RefreshTokens(tokenRefreshRequest.ExpiredJwt, tokenRefreshRequest.RefreshToken);
        if (result.Unsccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);
        else return Ok(new TokenRefreshResponse
        {
            NewJwt = result.Value.Jwt,
            NewRefreshToken = result.Value.RefreshToken
        });
    }
}