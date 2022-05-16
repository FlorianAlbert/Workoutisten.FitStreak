﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RequestRegistration([FromBody] RegistrationRequest registrationRequest)
    {
        if(string.IsNullOrEmpty(registrationRequest?.Email) ||
           string.IsNullOrEmpty(registrationRequest.Password) ||
           string.IsNullOrEmpty(registrationRequest.FirstName) ||
           string.IsNullOrEmpty(registrationRequest.LastName)) return BadRequest();

        var canRegister = await RegistrationService.CanRegisterAsync(registrationRequest.Email);
        if (!canRegister) return Conflict();

        var successful = await RegistrationService.RegisterAsync(registrationRequest.Email, 
                                                                 registrationRequest.Password, 
                                                                 registrationRequest.FirstName,
                                                                 registrationRequest.LastName);
        if (successful) return Ok();
        else return BadRequest();
    }

    [HttpPost]
    [Route("confirm/{userId}", Name = nameof(ConfirmRegistration))]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmRegistration(Guid userId)
    {
        if(userId == Guid.Empty) return BadRequest();

        var successful = await RegistrationService.ConfirmRegistrationAsync(userId);
        if (successful) return Ok();
        else return BadRequest();
    }
}