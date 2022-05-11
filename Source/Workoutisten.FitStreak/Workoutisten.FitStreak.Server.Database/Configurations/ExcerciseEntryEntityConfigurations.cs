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

            builder.Property(x => x.Category)
                   .HasConversion(v => v.ToString(),
                                  v => (ExerciseCategory)Enum.Parse(typeof(ExerciseCategory), v));

            builder.HasDiscriminator(x => x.Category)
                   .HasValue<CardioExerciseEntry>(ExerciseCategory.Cardio)
                   .HasValue<StrengthExerciseEntry>(ExerciseCategory.Strength);
        }
    }
}
