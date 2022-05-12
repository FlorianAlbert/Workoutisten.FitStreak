using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class EmailEntityConfigurations : BaseEntityConfigurations<Email>
    {
        public override void Configure(EntityTypeBuilder<Email> builder)
        {
            base.Configure(builder);

            builder.HasMany(x => x.Receivers)
                   .WithMany(x => x.Emails);
        }
    }
}
