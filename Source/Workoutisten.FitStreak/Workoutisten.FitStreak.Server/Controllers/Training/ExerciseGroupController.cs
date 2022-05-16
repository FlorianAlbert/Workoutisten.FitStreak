using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.Training.Group;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/exerciseGroup")]
public class ExerciseGroupController : ControllerBase
{
    [HttpGet]
    [Route("{exerciseGroupId}", Name = nameof(GetExerciseGroup))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseGroup), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetExerciseGroup([FromRoute] Guid exerciseGroupId)
    {
        return BadRequest();
    }

    [HttpGet]
    [Route("", Name = nameof(GetExerciseGroups))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseGroup[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetExerciseGroups()
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("", Name = nameof(CreateExerciseGroup))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseGroup), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateExerciseGroup([FromBody] ExerciseGroup exerciseGroup)
    {
        return BadRequest();
    }

    [HttpPut]
    [Route("", Name = nameof(UpdateExerciseGroup))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseGroup), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateExerciseGroup([FromBody] ExerciseGroup exerciseGroup)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("{exerciseGroupId}", Name = nameof(DeleteExerciseGroup))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteExerciseGroup([FromRoute] Guid exerciseGroupId)
    {
        return BadRequest();
    }
}
