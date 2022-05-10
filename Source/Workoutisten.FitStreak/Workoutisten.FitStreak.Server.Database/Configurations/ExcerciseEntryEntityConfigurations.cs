using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class ExerciseEntryEntityConfigurations : BaseEntityConfigurations<ExerciseEntry>
    {
        public override void Configure(EntityTypeBuilder<ExerciseEntry> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Exercise)
                   .WithMany(x => x.ExerciseEntries)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.WorkoutEntry)
                   .WithMany(x => x.ExerciseEntries)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Workout)
                   .WithMany(x => x.WorkoutContextExerciseEntries)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
