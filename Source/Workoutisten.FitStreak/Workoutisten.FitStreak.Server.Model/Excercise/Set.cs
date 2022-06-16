namespace Workoutisten.FitStreak.Server.Model.Excercise
{
    public abstract class Set : BaseEntity
    {
        public ExerciseCategory Category { get; set; }

        public virtual DoneExercise DoneExercise { get; set; }
    }
}
