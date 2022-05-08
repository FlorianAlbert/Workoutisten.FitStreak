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
            builder.HasOne(x => x.Workout)
                   .WithMany(x => x.ExerciseEntries)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => new { x.OrderNumber, x.ExerciseId, x.WorkoutId })
                   .IsUnique();
        }
    }
}
