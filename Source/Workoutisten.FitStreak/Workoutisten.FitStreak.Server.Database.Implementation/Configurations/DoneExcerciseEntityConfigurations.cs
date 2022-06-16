using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class DoneExerciseEntityConfigurations : BaseEntityConfigurations<DoneExercise>
    {
        public override void Configure(EntityTypeBuilder<DoneExercise> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Exercise)
                   .WithMany(x => x.ExerciseEntries)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ExerciseGroup)
                   .WithMany(x => x.DoneExercises)
                   .OnDelete(DeleteBehavior.SetNull);
            builder.HasMany(x => x.Sets)
                   .WithOne(x => x.DoneExercise)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.ExercisingUser)
                   .WithMany(x => x.DoneExercises)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
