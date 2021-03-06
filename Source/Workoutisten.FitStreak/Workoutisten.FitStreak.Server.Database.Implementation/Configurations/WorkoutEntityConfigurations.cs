using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Workout;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class WorkoutEntityConfigurations : BaseEntityConfigurations<Workout>
    {
        public override void Configure(EntityTypeBuilder<Workout> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => x.WorkoutExercises)
                   .WithOne(x => x.Workout)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasForeignKey(x => x.WorkoutId);
            builder.HasOne(x => x.Creator)
                   .WithMany(x => x.Workouts)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.ExerciseGroups)
                   .WithOne(x => x.Workout)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
