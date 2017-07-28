using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNotificationsWithArtistAndNotificationByUserId(string userId)
        {
            return _context.UserNotifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .Select(un => un.Notification)
                .Include(un => un.Gig.Artist)
                //.ProjectTo<NotificationDto>()
                .ToList();
        }
    }
}