namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public class CardioExerciseSetModel: BaseExerciseSetModel
    {
        public TimeSpan? Duration { get; set; }

        public double? Distance { get; set; }
    }
}
