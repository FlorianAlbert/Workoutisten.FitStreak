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

            builder.HasMany(x => x.ExerciseEntries)
                   .WithOne(x => x.Workout)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Creator)
                   .WithMany(x => x.Workouts)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
