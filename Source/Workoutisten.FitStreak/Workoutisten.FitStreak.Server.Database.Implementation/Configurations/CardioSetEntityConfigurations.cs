using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Excercise;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class CardioSetEntityConfigurations : BaseEntityConfigurations<CardioSet>
    {
        public override void Configure(EntityTypeBuilder<CardioSet> builder)
        {
            builder.HasBaseType<Set>();

            builder.Property(x => x.Duration)
                   .HasConversion(v => v.Ticks,
                                  v => TimeSpan.FromTicks(v));
        }
    }
}
