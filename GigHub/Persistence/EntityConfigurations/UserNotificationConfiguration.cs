using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : EntityTypeConfiguration<UserNotification>
    {
        public UserNotificationConfiguration()
        {

            HasKey(un => new { un.UserId, un.NotificationId });

            //modelBuilder.Entity<UserNotification>()
            //    .HasRequired(n => n.User)
            //    .WithMany(u => u.UserNotifications)
            //    .WillCascadeOnDelete(false);

            HasRequired(n => n.User)
                .WithMany(u => u.UserNotifications)
                .WillCascadeOnDelete(false);
        }
    }
}