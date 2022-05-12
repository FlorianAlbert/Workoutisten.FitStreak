using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.DataTransferObjects.Training.Workout;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/workout")]
public class WorkoutController : ControllerBase
{
    [HttpGet]
    [Route("{workoutId}")]
    [Authorize]
    [ProducesResponseType(typeof(Workout), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWorkout([FromRoute] Guid workoutId)
    {
        return BadRequest();
    }

    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(Workout[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWorkouts()
    {
        return BadRequest();
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateWorkout([FromBody] Workout workout)
    {
        return BadRequest();
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateWorkout([FromBody] Workout workout)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("{workoutId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteWorkout([FromRoute] Guid workoutId)
    {
        return BadRequest();
    }
}