using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class StrengthExerciseEntryEntityConfigurations : BaseEntityConfigurations<StrengthExerciseEntry>
    {
        public override void Configure(EntityTypeBuilder<StrengthExerciseEntry> builder)
        {
            builder.HasBaseType<ExerciseEntry>();
        }
    }
}
