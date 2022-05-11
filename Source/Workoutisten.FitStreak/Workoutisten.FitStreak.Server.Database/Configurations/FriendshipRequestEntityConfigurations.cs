using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workoutisten.FitStreak.Server.Model.Account;

namespace Workoutisten.FitStreak.Server.Database.Implementation.Configurations
{
    public class FriendshipRequestEntityConfigurations : BaseEntityConfigurations<FriendshipRequest>
    {
        public override void Configure(EntityTypeBuilder<FriendshipRequest> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.RequestingUser)
                   .WithMany(x => x.OutgoingFriendshipRequests)
                   .OnDelete(DeleteBehavior.Cascade)
                   .HasForeignKey(x => x.RequestingUserId);
            builder.HasOne(x => x.RequestedUser)
                   .WithMany(x => x.IngoingFriendshipRequests)
                   .OnDelete(DeleteBehavior.Restrict)
                   .HasForeignKey(x => x.RequestedUserId);
            builder.HasIndex(x => new { x.RequestedUserId, x.RequestingUserId })
                   .IsUnique();
        }
    }
}
