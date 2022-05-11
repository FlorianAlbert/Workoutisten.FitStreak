using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class CardioExerciseEntryEntityConfigurations : BaseEntityConfigurations<CardioExerciseEntry>
    {
        public override void Configure(EntityTypeBuilder<CardioExerciseEntry> builder)
        {
            builder.HasBaseType<ExerciseEntry>();

            builder.Property(x => x.Duration)
                   .HasConversion(v => v.Ticks,
                                  v => TimeSpan.FromTicks(v));
        }
    }
}
