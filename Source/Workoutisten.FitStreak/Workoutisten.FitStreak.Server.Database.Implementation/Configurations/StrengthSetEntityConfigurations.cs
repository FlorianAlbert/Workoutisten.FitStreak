using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class StrengthSetEntityConfigurations : BaseEntityConfigurations<StrengthSet>
    {
        public override void Configure(EntityTypeBuilder<StrengthSet> builder)
        {
            builder.HasBaseType<Set>();
        }
    }
}
