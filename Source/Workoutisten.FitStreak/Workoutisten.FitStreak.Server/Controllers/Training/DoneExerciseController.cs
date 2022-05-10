using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.DataTransferObjects.Training.DoneExercise;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/doneExercise")]
public class DoneExerciseController : ControllerBase
{
    [HttpPost]
    [Route("cardio")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateDoneCardioExercise([FromBody] DoneCardioExercise doneCardioExercise)
    {
        return BadRequest();
    }

    [HttpPut]
    [Route("cardio")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateDoneCardioExercise([FromBody] DoneCardioExercise doneCardioExercise)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("cardio/{doneCardioExerciseId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteDoneCardioExercise([FromRoute] Guid doneCardioExerciseId)
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("strength")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateDoneStrengthExercise([FromBody] DoneStrengthExercise doneStrengthExercise)
    {
        return BadRequest();
    }

    [HttpPut]
    [Route("strength")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateDoneStrengthExercise([FromBody] DoneStrengthExercise doneStrengthExercise)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("strength/{doneStrengthExerciseId}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteDoneStrengthExercise([FromRoute] Guid doneStrengthExerciseId)
    {
        return BadRequest();
    }
}

