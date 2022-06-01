using Workoutisten.FitStreak.Server.Service.Interface.Converter;
using SetEntity = Workoutisten.FitStreak.Server.Model.Excercise.Set;
using StrengthSetEntity = Workoutisten.FitStreak.Server.Model.Excercise.StrengthSet;
using CardioSetEntity = Workoutisten.FitStreak.Server.Model.Excercise.CardioSet;
using SetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.Set;
using StrengthSetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.StrengthSet;
using CardioSetDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.DoneExercise.CardioSet;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Converter.Training;

public class SetConverter : IConverter<SetEntity, SetDto>
{
    public async Task<SetDto> ToDto(SetEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        return entity switch
        {
            StrengthSetEntity strengthSet => new StrengthSetDto
            {
                Id = strengthSet.Id,
                DoneExerciseId = strengthSet.DoneExercise.Id,
                Weight = strengthSet.Weight,
                Repetitions = strengthSet.Repetitions
            },
            CardioSetEntity cardioSet => new CardioSetDto
            {
                Id = cardioSet.Id,
                DoneExerciseId = cardioSet.DoneExercise.Id,
                Distance = cardioSet.Distance,
                Duration = cardioSet.Duration
            },
            _ => throw new ArgumentException("Entity type is not suported.", nameof(entity)),
        };
    }

    public Task<SetEntity> ToEntity(SetDto dto)
    {
        throw new NotImplementedException();
    }
}
