using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.Training.Exercise;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/exercise")]
public class ExerciseController : ControllerBase
{
    [HttpGet]
    [Route("{exerciseId}", Name = nameof(GetExercise))]
    [Authorize]
    [ProducesResponseType(typeof(Exercise), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetExercise([FromRoute] Guid exerciseId)
    {
        return BadRequest();
    }

    [HttpGet]
    [Authorize]
    [Route("", Name = nameof(GetExercises))]
    [ProducesResponseType(typeof(Exercise[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetExercises()
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("", Name = nameof(CreateExercise))]
    [Authorize]
    [ProducesResponseType(typeof(Exercise) ,StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateExercise([FromBody] Exercise exercise)
    {
        return BadRequest();
    }

    [HttpPut]
    [Route("", Name = nameof(UpdateExercise))]
    [Authorize]
    [ProducesResponseType(typeof(Exercise), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateExercise([FromBody] Exercise exercise)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("{exerciseId}", Name = nameof(DeleteExercise))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteExercise([FromRoute] Guid exerciseId)
    {
        return BadRequest();
    }
}