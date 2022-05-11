using Microsoft.EntityFrameworkCore;
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
            builder.HasMany(x => x.Exercises)
                   .WithOne(x => x.Creator)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Workouts)
                   .WithOne(x => x.Creator)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Emails)
                   .WithMany(x => x.Receivers);
            builder.HasMany(x => x.MyFriends)
                   .WithMany(x => x.UsersIAmFriendOf);
            builder.HasMany(x => x.IngoingFriendshipRequests)
                   .WithOne(x => x.RequestedUser)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(x => x.RequestedUserId);
            builder.HasMany(x => x.OutgoingFriendshipRequests)
                   .WithOne(x => x.RequestingUser)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasForeignKey(x => x.RequestingUserId);
        }
    }
}
