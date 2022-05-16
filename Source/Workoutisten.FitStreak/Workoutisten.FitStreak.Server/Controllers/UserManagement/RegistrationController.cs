using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Registration;
using Workoutisten.FitStreak.Server.Service.Interface.Data;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> RequestRegistration([FromBody] RegistrationRequest registrationRequest)
    {
        if(string.IsNullOrEmpty(registrationRequest?.Email) ||
           string.IsNullOrEmpty(registrationRequest.Password) ||
           string.IsNullOrEmpty(registrationRequest.FirstName) ||
           string.IsNullOrEmpty(registrationRequest.LastName)) return BadRequest();

        var canRegisterResult = await RegistrationService.CanRegisterAsync(registrationRequest.Email);
        if (canRegisterResult.Status == ResultStatus.ServerError) return Problem(statusCode: StatusCodes.Status503ServiceUnavailable);
        if (canRegisterResult.Status != ResultStatus.Successful) 
            throw new NotImplementedException($"{nameof(RegistrationService.CanRegisterAsync)} should not produce ResultStatus {canRegisterResult.Status}!");
        if (!canRegisterResult.Data) return Conflict();

        var registrationResult = await RegistrationService.RegisterAsync(registrationRequest.Email, 
                                                                 registrationRequest.Password, 
                                                                 registrationRequest.FirstName,
                                                                 registrationRequest.LastName);
        if (registrationResult.Status == ResultStatus.ServerError) return Problem(statusCode: StatusCodes.Status503ServiceUnavailable);
        if (registrationResult.Status != ResultStatus.Successful) 
            throw new NotImplementedException($"{nameof(RegistrationService.RegisterAsync)} should not produce ResultStatus {registrationResult.Status}!");
        if (registrationResult.Data) return Ok();
        else return BadRequest();
    }

    [HttpPost]
    [Route("confirm/{userId}", Name = nameof(ConfirmRegistration))]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> ConfirmRegistration(Guid userId)
    {
        if(userId == Guid.Empty) return BadRequest();

        var confirmationResult = await RegistrationService.ConfirmRegistrationAsync(userId);
        switch (confirmationResult.Status)
        {
            case ResultStatus.BadRequest:
                return BadRequest();
            case ResultStatus.ServerError:
                return Problem(statusCode: StatusCodes.Status503ServiceUnavailable);
            case ResultStatus.Successful:
                if (confirmationResult.Data) return Ok();
                else return BadRequest();
            default:
                throw new NotImplementedException($"{nameof(RegistrationService.ConfirmRegistrationAsync)} should not produce ResultStatus {confirmationResult.Status}!");
        }
    }
}