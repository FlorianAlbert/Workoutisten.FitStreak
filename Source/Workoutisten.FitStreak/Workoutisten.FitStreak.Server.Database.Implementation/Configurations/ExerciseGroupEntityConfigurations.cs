using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Workout;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class ExerciseGroupEntityConfigurations : BaseEntityConfigurations<ExerciseGroup>
    {
        public override void Configure(EntityTypeBuilder<ExerciseGroup> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => x.DoneExercises)
                   .WithOne(x => x.ExerciseGroup)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Workout)
                   .WithMany(x => x.ExerciseGroups)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(x => x.Participants)
                   .WithMany(x => x.ParticipatedExerciseGroups);
        }
    }
}
