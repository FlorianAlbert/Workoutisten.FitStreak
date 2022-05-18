using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Service.Interface.UserManagement;
using Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person;
using UserEntity = Workoutisten.FitStreak.Server.Model.Account.User;
using UserDto = Workoutisten.FitStreak.Server.Outbound.Model.UserManagement.Person.User;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;

namespace Workoutisten.FitStreak.Server.Controllers.UserManagement;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private IUserService UserService { get; }
    private IConverterWrapper Converter { get; }

    public UserController(IUserService userService, IConverterWrapper converter)
    {
        UserService = userService ?? throw new ArgumentNullException(nameof(userService));
        Converter = converter ?? throw new ArgumentNullException(nameof(converter));
    }

    [HttpPut]
    [Route("", Name = nameof(UpdateUser))]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UserUpdate userUpdate)
      {
        if (userUpdate?.Email is null &&
            userUpdate?.FirstName is null &&
            userUpdate?.LastName is null) return BadRequest("There was no content to update in the userUpdate!");

        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await UserService.UpdateUserAsync(userId.Value, 
                                                       userUpdate.Email, userUpdate.FirstName, userUpdate.LastName);
        if (result.Successful)
        {
            var userDto = await Converter.ToDto<UserEntity, UserDto>(result.Value);
            return Ok(userDto);
        }
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpDelete]
    [Route("{userToDeleteId}", Name = nameof(DeleteUser))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid userToDeleteId)
    {

        var ownUserId = await User.GetUserIdAsync();
        if (ownUserId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await UserService.DeleteUser(ownUserId.Value, userToDeleteId);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }
}
