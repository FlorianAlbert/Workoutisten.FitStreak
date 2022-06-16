using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutEntity = Workoutisten.FitStreak.Server.Model.Workout.Workout;
using WorkoutDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.Workout.Workout;
using ExerciseEntity = Workoutisten.FitStreak.Server.Model.Excercise.Exercise;
using ExerciseDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.Exercise.Exercise;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using Workoutisten.FitStreak.Server.Service.Interface.Training;
using Workoutisten.FitStreak.Server.Extensions;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/workout")]
public class WorkoutController : ControllerBase
{
    private IWorkoutService WorkoutService { get; }

    private IConverterWrapper Converter { get; }

    public WorkoutController(IWorkoutService workoutService, IConverterWrapper converter)
    {
        WorkoutService = workoutService ?? throw new ArgumentNullException(nameof(workoutService));
        Converter = converter ?? throw new ArgumentNullException(nameof(converter));
    }

    [HttpGet]
    [Route("{workoutId}", Name = nameof(GetWorkout))]
    [Authorize]
    [ProducesResponseType(typeof(WorkoutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetWorkout([FromRoute] Guid workoutId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await WorkoutService.GetWorkout(userId.Value, workoutId);
        if (result.Unsuccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);
        else
        {
            var dto = await Converter.ToDto<WorkoutEntity, WorkoutDto>(result.Value);
            return Ok(dto);
        }
    }

    [HttpGet]
    [Route("", Name = nameof(GetWorkouts))]
    [Authorize]
    [ProducesResponseType(typeof(WorkoutDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetWorkouts()
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await WorkoutService.GetWorkouts(userId.Value);
        if (result.Unsuccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);
        else
        {
            var dtos = result.Value?
                .Select(async w => await Converter.ToDto<WorkoutEntity, WorkoutDto>(w))
                .Select(t => t.Result)
                .ToArray();
            return Ok(dtos);
        }
    }

    [HttpGet]
    [Route("{workoutId}/exercises", Name = nameof(GetExercisesOfWorkout))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetExercisesOfWorkout([FromRoute] Guid workoutId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await WorkoutService.GetWorkout(userId.Value, workoutId);
        if (result.Unsuccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);
        
        var dtos = await Task.WhenAll(result.Value?.WorkoutExercises.Select(x => Converter.ToDto<ExerciseEntity, ExerciseDto>(x.Exercise)));

        return Ok(dtos);
    }

    [HttpPost]
    [Route("", Name = nameof(CreateWorkout))]
    [Authorize]
    [ProducesResponseType(typeof(WorkoutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> CreateWorkout([FromBody] WorkoutDto workout)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");
        if (string.IsNullOrEmpty(workout?.WorkoutName) ||
             string.IsNullOrEmpty(workout?.Description) ||
            workout.ExerciseIds is null)
            return BadRequest("One or more of the following values were empty: workoutName, exerciseIds!");

        var result = await WorkoutService
            .CreateWorkout(userId.Value, workout.WorkoutName, workout.Description, workout.ExerciseIds);
        if (result.Successful)
        {
            var dto = await Converter.ToDto<WorkoutEntity, WorkoutDto>(result.Value);
            return Ok(dto);
        }
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpPut]
    [Route("", Name = nameof(UpdateWorkout))]
    [Authorize]
    [ProducesResponseType(typeof(WorkoutDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> UpdateWorkout([FromBody] WorkoutDto workout)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");
        if (string.IsNullOrEmpty(workout?.WorkoutName) ||
            string.IsNullOrEmpty(workout?.Description) ||
            workout.ExerciseIds is null ||
            workout.WorkoutId is null)
            return BadRequest("One or more of the following values were empty: workoutName, workoutId, exerciseId!");

        var result = await WorkoutService
            .UpdateWorkout(userId.Value, workout.WorkoutId.Value, workout.WorkoutName, workout.Description, workout.ExerciseIds);
        if (result.Successful)
        {
            var dto = await Converter.ToDto<WorkoutEntity, WorkoutDto>(result.Value);
            return Ok(dto);
        }
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }

    [HttpDelete]
    [Route("{workoutId}", Name = nameof(DeleteWorkout))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteWorkout([FromRoute] Guid workoutId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await WorkoutService.DeleteWorkout(userId.Value, workoutId);
        if (result.Successful) return NoContent();
        else return Problem(statusCode: result.StatusCode, detail: result.Detail);
    }
}