using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Workout;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class WorkoutExerciseEntityConfigurations : BaseEntityConfigurations<WorkoutExercise>
    {
        public override void Configure(EntityTypeBuilder<WorkoutExercise> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Exercise)
                   .WithMany(x => x.WorkoutExercises)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(x => x.ExerciseId);
            builder.HasOne(x => x.Workout)
                   .WithMany(x => x.WorkoutExercises)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasForeignKey(x => x.WorkoutId);
            builder.HasIndex(x => new { x.ExerciseId, x.WorkoutId })
                   .IsUnique();
        }
    }
}
