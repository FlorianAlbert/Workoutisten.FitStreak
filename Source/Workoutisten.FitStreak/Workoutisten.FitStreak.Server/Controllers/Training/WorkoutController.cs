using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.Training.Workout;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/workout")]
public class WorkoutController : ControllerBase
{
    [HttpGet]
    [Route("{workoutId}", Name = nameof(GetWorkout))]
    [Authorize]
    [ProducesResponseType(typeof(Workout), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWorkout([FromRoute] Guid workoutId)
    {
        return BadRequest();
    }

    [HttpGet]
    [Route("", Name = nameof(GetWorkouts))]
    [Authorize]
    [ProducesResponseType(typeof(Workout[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWorkouts()
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("", Name = nameof(CreateWorkout))]
    [Authorize]
    [ProducesResponseType(typeof(Workout), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateWorkout([FromBody] Workout workout)
    {
        return BadRequest();
    }

    [HttpPut]
    [Route("", Name = nameof(UpdateWorkout))]
    [Authorize]
    [ProducesResponseType(typeof(Workout), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateWorkout([FromBody] Workout workout)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("{workoutId}", Name = nameof(DeleteWorkout))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteWorkout([FromRoute] Guid workoutId)
    {
        return BadRequest();
    }
}