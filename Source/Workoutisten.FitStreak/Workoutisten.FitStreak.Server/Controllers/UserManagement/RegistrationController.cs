using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Registration;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/registration")]
public class RegistrationController : ControllerBase
{
    private IRegistrationService RegistrationService { get; }

    public RegistrationController(IRegistrationService registrationService)
    {
        RegistrationService = registrationService ?? throw new ArgumentNullException(nameof(registrationService));
    }

    [HttpPost]
    [Route("request", Name = nameof(RequestRegistration))]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> RequestRegistration([FromBody] RegistrationRequest registrationRequest)
    {
        if (string.IsNullOrEmpty(registrationRequest?.Email) ||
           string.IsNullOrEmpty(registrationRequest.Password) ||
           string.IsNullOrEmpty(registrationRequest.FirstName) ||
           string.IsNullOrEmpty(registrationRequest.LastName))  
           return BadRequest("One or more of the following values were empty: email, password, firstname, lastname!");

        var canRegisterResult = await RegistrationService.CanRegisterAsync(registrationRequest.Email);
        if (canRegisterResult.Unsccessful) return Problem(statusCode: canRegisterResult.StatusCode, detail: canRegisterResult.Detail);

        var result = await RegistrationService.RegisterAsync(registrationRequest.Email, 
                                                                 registrationRequest.Password, 
                                                                 registrationRequest.FirstName,
                                                                 registrationRequest.LastName);
        if (result.Successful) return Ok();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpPost]
    [Route("confirm/{registrationConfirmationKey}", Name = nameof(ConfirmRegistration))]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> ConfirmRegistration([FromRoute] string registrationConfirmationKey)
    {
        var result = await RegistrationService.ConfirmRegistrationAsync(registrationConfirmationKey);
        if (result.Successful) return Ok();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }
}