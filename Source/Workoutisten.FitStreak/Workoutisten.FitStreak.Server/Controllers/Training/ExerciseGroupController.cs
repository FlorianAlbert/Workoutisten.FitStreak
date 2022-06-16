using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using Workoutisten.FitStreak.Server.Service.Interface.Training;
using ExerciseGroupEntity = Workoutisten.FitStreak.Server.Model.Workout.ExerciseGroup;
using ExerciseGroupDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.Group.ExerciseGroup;
using DoneExerciseEntity = Workoutisten.FitStreak.Server.Model.Excercise.DoneExercise;
using DoneExerciseDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.DoneExercise;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/exerciseGroup")]
public class ExerciseGroupController : ControllerBase
{
    private IConverterWrapper ConverterWrapper { get; }
    private IExerciseGroupService ExerciseGroupService { get; }

    public ExerciseGroupController(IConverterWrapper converterWrapper, IExerciseGroupService exerciseGroupService)
    {
        ConverterWrapper = converterWrapper ?? throw new ArgumentNullException(nameof(converterWrapper));
        ExerciseGroupService = exerciseGroupService ?? throw new ArgumentNullException(nameof(exerciseGroupService));
    }

    [HttpGet]
    [Route("{exerciseGroupId}", Name = nameof(GetExerciseGroup))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseGroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetExerciseGroup([FromRoute] Guid exerciseGroupId)
    {
        var userId = await User.GetUserIdAsync();
        if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

        var exerciseGroupResult = await ExerciseGroupService.GetExerciseGroupForUser(userId.Value, exerciseGroupId);

        if (exerciseGroupResult.Unsuccessful) return Problem(statusCode: exerciseGroupResult.StatusCode, detail: exerciseGroupResult.Detail);

        var set = await ConverterWrapper.ToDto<ExerciseGroupEntity, ExerciseGroupDto>(exerciseGroupResult.Value);

        return Ok(set);
    }

    [HttpGet]
    [Route("", Name = nameof(GetExerciseGroups))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseGroupDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetExerciseGroups()
    {
        var userId = await User.GetUserIdAsync();
        if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

        var exerciseGroupsResult = await ExerciseGroupService.GetAllExerciseGroupsForUser(userId.Value);

        if (exerciseGroupsResult.Unsuccessful) return Problem(statusCode: exerciseGroupsResult.StatusCode, detail: exerciseGroupsResult.Detail);

        var sets = await Task.WhenAll(exerciseGroupsResult.Value.Select(x => ConverterWrapper.ToDto<ExerciseGroupEntity, ExerciseGroupDto>(x)));

        return Ok(sets);
    }

    [HttpGet]
    [Route("{exerciseGroupId}/doneExercises", Name = nameof(GetDoneExercisesOfExerciseGroup))]
    [Authorize]
    [ProducesResponseType(typeof(DoneExerciseDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetDoneExercisesOfExerciseGroup([FromRoute] Guid exerciseGroupId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await ExerciseGroupService.GetExerciseGroupForUser(userId.Value, exerciseGroupId);
        if (result.Unsuccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);

        var dtos = await Task.WhenAll(result.Value?.DoneExercises.Select(x => ConverterWrapper.ToDto<DoneExerciseEntity, DoneExerciseDto>(x)));

        return Ok(dtos);
    }

    [HttpPost]
    [Route("", Name = nameof(CreateExerciseGroup))]
    [Authorize]
    [ProducesResponseType(typeof(ExerciseGroupDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> CreateExerciseGroup([FromBody] ExerciseGroupDto exerciseGroup)
    {
        var userId = await User.GetUserIdAsync();
        if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

        var exerciseGroupResult = await ExerciseGroupService.CreateNewExerciseGroup(userId.Value, exerciseGroup.GroupName, exerciseGroup.WorkoutId);

        if (exerciseGroupResult.Unsuccessful) return Problem(statusCode: exerciseGroupResult.StatusCode, detail: exerciseGroupResult.Detail);

        var createdExerciseGroup = await ConverterWrapper.ToDto<ExerciseGroupEntity, ExerciseGroupDto>(exerciseGroupResult.Value);

        return Ok(createdExerciseGroup);
    }

    [HttpDelete]
    [Route("{exerciseGroupId}", Name = nameof(DeleteExerciseGroup))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> DeleteExerciseGroup([FromRoute] Guid exerciseGroupId)
    {
        var userId = await User.GetUserIdAsync();
        if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

        var exerciseGroupResult = await ExerciseGroupService.DeleteExerciseGroup(userId.Value, exerciseGroupId);

        if (exerciseGroupResult.Unsuccessful) return Problem(statusCode: exerciseGroupResult.StatusCode, detail: exerciseGroupResult.Detail);

        return NoContent();
    }
}
