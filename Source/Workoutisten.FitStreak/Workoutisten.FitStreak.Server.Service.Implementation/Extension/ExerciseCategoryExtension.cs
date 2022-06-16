using ExerciseCategoryDto = Workoutisten.FitStreak.Server.Outbound.Model.Training.Exercise.ExerciseCategory;
using ExerciseCategoryEntity = Workoutisten.FitStreak.Server.Model.Excercise.ExerciseCategory;

namespace Workoutisten.FitStreak.Server.Service.Implementation.Extension;

public static class ExerciseCategoryExtension
{

    public static ExerciseCategoryDto ToDto(this ExerciseCategoryEntity category) =>
     category switch
     {
         ExerciseCategoryEntity.Cardio => ExerciseCategoryDto.Cardio,
         ExerciseCategoryEntity.Strength => ExerciseCategoryDto.Strength,
         _ => throw new NotImplementedException()
     };

    public static ExerciseCategoryEntity ToEntity(this ExerciseCategoryDto category) =>
     category switch
     {
         ExerciseCategoryDto.Cardio => ExerciseCategoryEntity.Cardio,
         ExerciseCategoryDto.Strength => ExerciseCategoryEntity.Strength,
         _ => throw new NotImplementedException()
     };
}
