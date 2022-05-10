using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.DataTransferObjects.Training.Group;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/exerciseGroup")]
public class ExerciseGroupController : ControllerBase
{

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateExerciseGroup([FromBody] ExerciseGroup exerciseGroup)
    {
        return BadRequest();
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateExerciseGroup([FromBody] ExerciseGroup exerciseGroup)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("{exerciseGroupId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteExerciseGroup([FromRoute] Guid exerciseGroupId)
    {
        return BadRequest();
    }
}
