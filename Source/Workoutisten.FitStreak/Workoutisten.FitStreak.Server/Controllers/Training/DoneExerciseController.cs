using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/doneExercise")]
public class DoneExerciseController : ControllerBase
{
    [HttpGet]
    [Route("cardio/{doneCardioExerciseId}", Name = nameof(GetDoneCardioExercise))]
    [Authorize]
    [ProducesResponseType(typeof(DoneCardioExercise), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDoneCardioExercise([FromRoute] Guid doneCardioExerciseId)
    {
        return BadRequest();
    }

    [HttpGet]
    [Route("cardio", Name = nameof(GetDoneCardioExercises))]
    [Authorize]
    [ProducesResponseType(typeof(DoneCardioExercise[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDoneCardioExercises()
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("cardio", Name = nameof(CreateDoneCardioExercise))]
    [Authorize]
    [ProducesResponseType(typeof(DoneCardioExercise) ,StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateDoneCardioExercise([FromBody] DoneCardioExercise doneCardioExercise)
    {
        return BadRequest();
    }

    [HttpPut]
    [Route("cardio", Name = nameof(UpdateDoneCardioExercise))]
    [Authorize]
    [ProducesResponseType(typeof(DoneCardioExercise), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateDoneCardioExercise([FromBody] DoneCardioExercise doneCardioExercise)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("cardio/{doneCardioExerciseId}", Name = nameof(DeleteDoneCardioExercise))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteDoneCardioExercise([FromRoute] Guid doneCardioExerciseId)
    {
        return BadRequest();
    }

    [HttpGet]
    [Route("strength/{doneStrengthExerciseId}", Name = nameof(GetDoneStrengthExercise))]
    [Authorize]
    [ProducesResponseType(typeof(DoneStrengthExercise), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDoneStrengthExercise([FromRoute] Guid doneStrengthExerciseId)
    {
        return BadRequest();
    }

    [HttpGet]
    [Route("strength", Name = nameof(GetDoneStrengthExercises))]
    [Authorize]
    [ProducesResponseType(typeof(DoneStrengthExercise[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetDoneStrengthExercises()
    {
        return BadRequest();
    }

    [HttpPost]
    [Route("strength", Name = nameof(CreateDoneStrengthExercise))]
    [Authorize]
    [ProducesResponseType(typeof(DoneStrengthExercise), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateDoneStrengthExercise([FromBody] DoneStrengthExercise doneStrengthExercise)
    {
        return BadRequest();
    }

    [HttpPut]
    [Route("strength", Name = nameof(UpdateDoneStrengthExercise))]
    [Authorize]
    [ProducesResponseType(typeof(DoneStrengthExercise), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateDoneStrengthExercise([FromBody] DoneStrengthExercise doneStrengthExercise)
    {
        return BadRequest();
    }

    [HttpDelete]
    [Route("strength/{doneStrengthExerciseId}", Name = nameof(DeleteDoneStrengthExercise))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteDoneStrengthExercise([FromRoute] Guid doneStrengthExerciseId)
    {
        return BadRequest();
    }
}

