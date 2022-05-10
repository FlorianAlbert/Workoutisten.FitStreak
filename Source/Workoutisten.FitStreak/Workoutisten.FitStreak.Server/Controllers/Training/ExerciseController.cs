using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.DataTransferObjects.Training.Exercise;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/exercise")]
public class ExerciseController : ControllerBase
{
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateExercise([FromBody] Exercise exercise)
    {
        return BadRequest();
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateExercise([FromBody] Exercise exercise)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("{exerciseId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteExercise([FromRoute] Guid exerciseId)
    {
        return BadRequest();
    }
}