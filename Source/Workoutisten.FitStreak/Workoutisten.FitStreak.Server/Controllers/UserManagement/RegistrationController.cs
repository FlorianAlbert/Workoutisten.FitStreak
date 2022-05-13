using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Registration;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/registration")]
public class RegistrationController : ControllerBase
{
    private IUserService UserService { get; }

    private IRegistrationService RegistrationService { get; }

    public RegistrationController(IUserService userService, IRegistrationService registrationService)
    {
        UserService = userService;
        RegistrationService = registrationService;
    }

    [HttpPost]
    [Route("request")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RequestRegistration([FromBody] RegistrationRequest registrationRequest)
    {
        if(registrationRequest?.Email is null || registrationRequest?.Password is null) return BadRequest();

        //ToDo UseCase auslagern
        //var users = await UserService.GetAllUsersAsync();
        //if (users.Any(user => user.NormalizedEmail == registrationRequest.Email.Normalize())) return Conflict();

        var successful = await RegistrationService.RequestRegistrationAsync(registrationRequest.Email, registrationRequest.Password);
        if (successful) return Ok();
        else return BadRequest();
    }

    [HttpPost]
    [Route("confirm/{confirmationId}")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmRegistration(Guid confirmationId)
    {
        if(confirmationId == Guid.Empty) return BadRequest();

        var successful = await RegistrationService.ConfirmRegistrationAsync(confirmationId);
        if (successful) return Ok();
        else return BadRequest();
    }
}