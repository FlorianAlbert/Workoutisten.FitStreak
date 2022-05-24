using Workoutisten.FitStreak.Data.Enums;

namespace Workoutisten.FitStreak.Data.Models.Workout
{
    public class ExerciseModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public ExerciseCategoryEnum Category { get; set; }

        public string Description { get; set; }

    }
}
