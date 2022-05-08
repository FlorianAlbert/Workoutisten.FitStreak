using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class ExerciseEntityConfigurations : BaseEntityConfigurations<Exercise>
    {
        public override void Configure(EntityTypeBuilder<Exercise> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Category)
                   .HasConversion(v => v.ToString(),
                                  v => (ExerciseCategory)Enum.Parse(typeof(ExerciseCategory), v));
            builder.HasMany(x => x.ExerciseEntries)
                   .WithOne(x => x.Exercise)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Creator)
                   .WithMany(x => x.Exercises)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
