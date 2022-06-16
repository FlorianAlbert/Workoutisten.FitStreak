using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using Workoutisten.FitStreak.Server.Service.Interface.Training;
using DoneExerciseEntity = Workoutisten.FitStreak.Server.Model.Excercise.DoneExercise;
using DoneExerciseDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.DoneExercise;
using SetEntity = Workoutisten.FitStreak.Server.Model.Excercise.Set;
using SetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.Set;
using StrengthSetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.StrengthSet;
using CardioSetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.CardioSet;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Controllers.Training;

[ApiController]
[Route("api/doneExercise")]
public class DoneExerciseController : ControllerBase
{
    private IConverterWrapper ConverterWrapper { get; }
    private IDoneExerciseService DoneExerciseService { get; }

    public DoneExerciseController(IConverterWrapper converterWrapper, IDoneExerciseService doneExerciseService)
    {
        ConverterWrapper = converterWrapper ?? throw new ArgumentNullException(nameof(converterWrapper));
        DoneExerciseService = doneExerciseService ?? throw new ArgumentNullException(nameof(doneExerciseService));
    }

    [HttpGet]
    [Route("{doneExerciseId}", Name = nameof(GetDoneExercise))]
    [Authorize]
    [ProducesResponseType(typeof(DoneExerciseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetDoneExercise([FromRoute] Guid doneExerciseId)
    {
        var userId = await User.GetUserIdAsync();
        if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

        var doneExerciseResult = await DoneExerciseService.GetDoneExerciseForUser(userId.Value, doneExerciseId);

        if (doneExerciseResult.Unsuccessful) return Problem(statusCode: doneExerciseResult.StatusCode, detail: doneExerciseResult.Detail);

        var doneExercise = await ConverterWrapper.ToDto<DoneExerciseEntity, DoneExerciseDto>(doneExerciseResult.Value);

        return Ok(doneExercise);
    }

    [HttpGet(Name = nameof(GetDoneExercises))]
    [Authorize]
    [ProducesResponseType(typeof(DoneExerciseDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetDoneExercises()
    {
        var userId = await User.GetUserIdAsync();
        if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

        var doneExercisesResult = await DoneExerciseService.GetAllDoneExercisesForUser(userId.Value);

        if (doneExercisesResult.Unsuccessful) return Problem(statusCode: doneExercisesResult.StatusCode, detail: doneExercisesResult.Detail);

        var doneExercises = await Task.WhenAll(doneExercisesResult.Value?.Select(x => ConverterWrapper.ToDto<DoneExerciseEntity, DoneExerciseDto>(x)));

        return Ok(doneExercises);
    }

    [HttpGet]
    [Route("{doneExerciseId}/strengthSets", Name = nameof(GetStrengthSetsOfDoneExercise))]
    [Authorize]
    [ProducesResponseType(typeof(StrengthSetDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetStrengthSetsOfDoneExercise([FromRoute] Guid doneExerciseId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await DoneExerciseService.GetDoneExerciseForUser(userId.Value, doneExerciseId);
        if (result.Unsuccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);

        var dtos = (await Task.WhenAll(result.Value?.Sets.Where(x => x.Category == ExerciseCategory.Strength).Select(x => ConverterWrapper.ToDto<SetEntity, SetDto>(x)))).Select(x => x as StrengthSetDto).ToArray();

        return Ok(dtos);
    }

    [HttpGet]
    [Route("{doneExerciseId}/cardioSets", Name = nameof(GetCardioSetsOfDoneExercise))]
    [Authorize]
    [ProducesResponseType(typeof(CardioSetDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> GetCardioSetsOfDoneExercise([FromRoute] Guid doneExerciseId)
    {
        var userId = await User.GetUserIdAsync();
        if (userId is null) return BadRequest("There was no userId present in the JWT!");

        var result = await DoneExerciseService.GetDoneExerciseForUser(userId.Value, doneExerciseId);
        if (result.Unsuccessful) return Problem(statusCode: result.StatusCode, detail: result.Detail);

        var dtos = (await Task.WhenAll(result.Value?.Sets.Where(x => x.Category == ExerciseCategory.Cardio).Select(x => ConverterWrapper.ToDto<SetEntity, SetDto>(x)))).Select(x => x as CardioSetDto).ToArray();

        return Ok(dtos);
    }

    [HttpPost(Name = nameof(CreateDoneExercise))]
    [Authorize]
    [ProducesResponseType(typeof(DoneExerciseDto) ,StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> CreateDoneExercise([FromBody] DoneExerciseDto doneExercise)
    {
        var userId = await User.GetUserIdAsync();
        if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

        var doneExerciseResult = await DoneExerciseService.CreateDoneExercise(userId.Value, doneExercise.ExerciseId, doneExercise.ExerciseGroupId);

        if (doneExerciseResult.Unsuccessful) return Problem(statusCode: doneExerciseResult.StatusCode, detail: doneExerciseResult.Detail);

        var createdDoneExercise = await ConverterWrapper.ToDto<DoneExerciseEntity, DoneExerciseDto>(doneExerciseResult.Value);

        return Ok(createdDoneExercise);
    }

    [HttpDelete]
    [Route("{doneCardioExerciseId}", Name = nameof(DeleteDoneExercise))]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> DeleteDoneExercise([FromRoute] Guid doneExerciseId)
    {
        var userId = await User.GetUserIdAsync();
        if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

        var doneExerciseResult = await DoneExerciseService.DeleteDoneExercise(userId.Value, doneExerciseId);

        if (doneExerciseResult.Unsuccessful) return Problem(statusCode: doneExerciseResult.StatusCode, detail: doneExerciseResult.Detail);

        return NoContent();
    }
}

