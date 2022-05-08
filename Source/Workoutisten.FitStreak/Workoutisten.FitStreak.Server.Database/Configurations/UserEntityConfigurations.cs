using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class UserEntityConfigurations : BaseEntityConfigurations<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.PasswordForgottenKey)
                   .IsUnique();
        }
    }
}
