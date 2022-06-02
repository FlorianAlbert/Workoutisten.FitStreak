using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class SetEntityConfigurations : BaseEntityConfigurations<Set>
    {
        public override void Configure(EntityTypeBuilder<Set> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.DoneExercise)
                   .WithMany(x => x.Sets)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Category)
                   .HasConversion(v => v.ToString(),
                                  v => (ExerciseCategory)Enum.Parse(typeof(ExerciseCategory), v));

            builder.HasDiscriminator(x => x.Category)
                   .HasValue<CardioSet>(ExerciseCategory.Cardio)
                   .HasValue<StrengthSet>(ExerciseCategory.Strength);
        }
    }
}
