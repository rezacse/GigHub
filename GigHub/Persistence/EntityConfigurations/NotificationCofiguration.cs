using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class NotificationCofiguration : EntityTypeConfiguration<Notification>
    {
        public NotificationCofiguration()
        {
            HasRequired(n => n.Gig);
        }
    }
}