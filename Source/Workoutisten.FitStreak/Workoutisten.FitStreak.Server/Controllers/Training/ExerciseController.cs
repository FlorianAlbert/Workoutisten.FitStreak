using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using Workoutisten.FitStreak.Server.Service.Interface.Training;
using ExerciseEntity = Workoutisten.FitStreak.Server.Model.Excercise.Exercise;
using ExerciseDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.Exercise.Exercise;
using Workoutisten.FitStreak.Server.Service.Implementation.Extension;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/exercise")]
public class ExerciseController : ControllerBase
{
    private IExerciseService ExerciseService { get; }

    private IConverterWrapper Converter { get; }

    public ExerciseController(IExerciseService exerciseService, IConverterWrapper converter)
    {
        ExerciseService = exerciseService ?? throw new ArgumentNullException(nameof(exerciseService));
        Converter = converter ?? throw new ArgumentNullException(nameof(converter));
    }

    [HttpGet]
    [Route("{exerciseId}", Name = nameof(GetExercise))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetExercise([FromRoute] Guid exerciseId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await ExerciseService.GetExercise(userId.Value, exerciseId);
        if (result.Unsccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);
        else
        {
            var dto = await Converter.ToDto<ExerciseEntity, ExerciseDto>(result.Value);
            return Ok(dto);
        }
    }

    [HttpGet]
    [Authorize]
    [Route("", Name = nameof(GetExercises))]
    [ProducesResponseType(typeof(ExerciseDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetExercises()
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await ExerciseService.GetExercises(userId.Value);
        if (result.Unsccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);
        else
        {
            var dtos = result.Value?
                .Select(async e => await Converter.ToDto<ExerciseEntity, ExerciseDto>(e))
                .Select(t => t.Result)
                .ToArray();
            return Ok(dtos);
        }
    }

    [HttpPost]
    [Route("", Name = nameof(CreateExercise))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> CreateExercise([FromBody] ExerciseDto exercise)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");
        if (string.IsNullOrEmpty(exercise?.Name) ||
            string.IsNullOrEmpty(exercise.Description)) 
            return BadRequest("One or more of the following values were empty: name, description!");

        var result = await ExerciseService
            .CreateExercise(userId.Value, exercise.Name, exercise.Description, exercise.ExerciseCategory.ToEntity());
        if (result.Successful) 
        {
            var dto = await Converter.ToDto<ExerciseEntity, ExerciseDto>(result.Value);
            return Ok(dto);
        }
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpPut]
    [Route("", Name = nameof(UpdateExercise))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> UpdateExercise([FromBody] ExerciseDto exercise)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");
        if (string.IsNullOrEmpty(exercise?.Name) ||
            string.IsNullOrEmpty(exercise.Description) ||
            exercise.ExerciseId is null)
            return BadRequest("One or more of the following values were empty: name, description, exerciseId!");

        var result = await ExerciseService
            .UpdateExercise(userId.Value, exercise.ExerciseId.Value, exercise.Name, exercise.Description, exercise.ExerciseCategory.ToEntity());
        if (result.Successful)
        {
            var dto = await Converter.ToDto<ExerciseEntity, ExerciseDto>(result.Value);
            return Ok(dto);
        }
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpDelete]
    [Route("{exerciseId}", Name = nameof(DeleteExercise))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> DeleteExercise([FromRoute] Guid exerciseId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await ExerciseService.DeleteExercise(userId.Value, exerciseId);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }
}