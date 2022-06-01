using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Workoutisten.FitStreak.Server.Extensions;
using Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise;
using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using Workoutisten.FitStreak.Server.Service.Interface.Training;
using SetEntity = Workoutisten.FitStreak.Server.Model.Excercise.Set;
using SetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.Set;

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
        [ProducesResponseType(typeof(StrengthSet), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> CreateStrengthSet([FromBody] StrengthSet set)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.CreateStrengthSet(userId.Value, set.DoneExerciseId, set.Weight, set.Repetitions);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var createdSet = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value);

            return Ok(createdSet);
        }

        [HttpPost]
        [Route("cardio", Name = nameof(CreateCardioSet))]
        [Authorize]
        [ProducesResponseType(typeof(CardioSet), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> CreateCardioSet([FromBody] CardioSet set)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.CreateCardioSet(userId.Value, set.DoneExerciseId, set.Distance, set.Duration);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var createdSet = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value);

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

        [HttpGet(Name = nameof(GetAllSets))]
        [Authorize]
        [ProducesResponseType(typeof(SetDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetAllSets()
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setsResult = await SetService.GetAllSetsForUser(userId.Value);

            if (setsResult.Unsuccessful) return Problem(statusCode: setsResult.StatusCode, detail: setsResult.Detail);

            var sets = await Task.WhenAll(setsResult.Value.Select(x => ConverterWrapper.ToDto<SetEntity, SetDto>(x)));

            return Ok(sets);
        }

        [HttpGet]
        [Route("{setId}", Name = nameof(GetSet))]
        [Authorize]
        [ProducesResponseType(typeof(SetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> GetSet([FromRoute] Guid setId)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.GetSetForUser(userId.Value, setId);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var set = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value);

            return Ok(set);
        }

        [HttpPut]
        [Route("strength", Name = nameof(UpdateStrengthSet))]
        [Authorize]
        [ProducesResponseType(typeof(StrengthSet), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UpdateStrengthSet([FromBody] StrengthSet set)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.UpdateStrengthSet(userId.Value, set.DoneExerciseId, set.Weight, set.Repetitions);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var updatedSet = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value);

            return Ok(updatedSet);
        }

        [HttpPut]
        [Route("cardio", Name = nameof(UpdateCardioSet))]
        [Authorize]
        [ProducesResponseType(typeof(CardioSet), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> UpdateCardioSet([FromBody] CardioSet set)
        {
            var userId = await User.GetUserIdAsync();
            if (!userId.HasValue) return BadRequest("There was no userId present in the JWT!");

            var setResult = await SetService.UpdateCardioSet(userId.Value, set.DoneExerciseId, set.Distance, set.Duration);

            if (setResult.Unsuccessful) return Problem(statusCode: setResult.StatusCode, detail: setResult.Detail);

            var updatedSet = await ConverterWrapper.ToDto<SetEntity, SetDto>(setResult.Value);

            return Ok(updatedSet);
        }
    }
}
