using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Workout;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class WorkoutEntryEntityConfigurations : BaseEntityConfigurations<WorkoutEntry>
    {
        public override void Configure(EntityTypeBuilder<WorkoutEntry> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => x.ExerciseEntries)
                   .WithOne(x => x.WorkoutEntry)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
