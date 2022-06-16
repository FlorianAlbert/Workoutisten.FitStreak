using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using Workoutisten.FitStreak.Server.Service.Interface.Training;
using SetEntity = Workoutisten.FitStreak.Server.Model.Excercise.Set;
using SetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.Set;
using StrengthSetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.StrengthSet;
using CardioSetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.CardioSet;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Controllers.Training
{
    [Route("api/set")]
    [ApiController]
    public class SetController : ControllerBase
    {
        public SetController(IConverterWrapper converterWrapper, ISetService setService)
        {
            ConverterWrapper = converterWrapper ?? throw new ArgumentNullException(nameof(converterWrapper));
            SetService = setService ?? throw new ArgumentNullException(nameof(setService));
        }

        private IConverterWrapper ConverterWrapper { get; }
        private ISetService SetService { get; }

        [HttpPost]
        [Route("strength", Name = nameof(CreateStrengthSet))]
        [Authorize]
        [ProducesResponseType(typeof(StrengthSetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> CreateStrengthSet([FromBody] StrengthSetDto set)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.CreateStrengthSet(userId.Value, set.DoneExerciseId, set.Weight, set.Repetitions);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var createdSet = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value) as StrengthSetDto;

            return Ok(createdSet);
        }

        [HttpPost]
        [Route("cardio", Name = nameof(CreateCardioSet))]
        [Authorize]
        [ProducesResponseType(typeof(CardioSetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> CreateCardioSet([FromBody] CardioSetDto set)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.CreateCardioSet(userId.Value, set.DoneExerciseId, set.Distance, TimeSpan.FromTicks(set.Ticks));

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var createdSet = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value) as CardioSetDto;

            return Ok(createdSet);
        }

        [HttpDelete(Name = nameof(DeleteSet))]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> DeleteSet([FromRoute] Guid setId)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.DeleteSet(userId.Value, setId);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            return NoContent();
        }

        [HttpGet("strengthSets", Name = nameof(GetAllStrengthSets))]
        [Authorize]
        [ProducesResponseType(typeof(StrengthSetDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAllStrengthSets()
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setsResult = await SetService.GetAllSetsForUser(userId.Value);

            if (setsResult.Unsuccessful) return Problem(statusCode: setsResult.StatusCode, detail: setsResult.Detail);

            var sets = (await Task.WhenAll(setsResult.Value.Where(x => x.Category == ExerciseCategory.Strength).Select(x => ConverterWrapper.ToDto<SetEntity, SetDto>(x)))).Select(x => x as StrengthSetDto).ToArray();

            return Ok(sets);
        }

        [HttpGet("cardioSets", Name = nameof(GetAllCardioSets))]
        [Authorize]
        [ProducesResponseType(typeof(CardioSetDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAllCardioSets()
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setsResult = await SetService.GetAllSetsForUser(userId.Value);

            if (setsResult.Unsuccessful) return Problem(statusCode: setsResult.StatusCode, detail: setsResult.Detail);

            var sets = (await Task.WhenAll(setsResult.Value.Where(x => x.Category == ExerciseCategory.Cardio).Select(x => ConverterWrapper.ToDto<SetEntity, SetDto>(x)))).Select(x => x as CardioSetDto).ToArray();

            return Ok(sets);
        }

        [HttpGet]
        [Route("strengthSet/{setId}", Name = nameof(GetStrengthSet))]
        [Authorize]
        [ProducesResponseType(typeof(StrengthSetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetStrengthSet([FromRoute] Guid setId)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.GetSetForUser(userId.Value, setId);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var set = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value);

            if (set is StrengthSetDto strengthSet) 
                return Ok(strengthSet);

            return Problem(statusCode: StatusCodes.Status404NotFound, detail: "The Set was not a StrengthSet.");
        }

        [HttpGet]
        [Route("cardioSet/{setId}", Name = nameof(GetCardioSet))]
        [Authorize]
        [ProducesResponseType(typeof(CardioSetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetCardioSet([FromRoute] Guid setId)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.GetSetForUser(userId.Value, setId);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var set = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value);

            if (set is CardioSetDto cardioSet)
                return Ok(cardioSet);

            return Problem(statusCode: StatusCodes.Status404NotFound, detail: "The Set was not a CardioSet.");
        }

        [HttpPut]
        [Route("strength", Name = nameof(UpdateStrengthSet))]
        [Authorize]
        [ProducesResponseType(typeof(StrengthSetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UpdateStrengthSet([FromBody] StrengthSetDto set)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.UpdateStrengthSet(userId.Value, set.DoneExerciseId, set.Weight, set.Repetitions);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var updatedSet = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value) as StrengthSetDto;

            return Ok(updatedSet);
        }

        [HttpPut]
        [Route("cardio", Name = nameof(UpdateCardioSet))]
        [Authorize]
        [ProducesResponseType(typeof(CardioSetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UpdateCardioSet([FromBody] CardioSetDto set)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.UpdateCardioSet(userId.Value, set.DoneExerciseId, set.Distance, TimeSpan.FromTicks(set.Ticks));

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var updatedSet = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value) as CardioSetDto;

            return Ok(updatedSet);
        }
    }
}
