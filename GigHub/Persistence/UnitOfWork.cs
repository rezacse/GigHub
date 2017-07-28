using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IGigRepository GigRepository { get; }
        public IAttendanceRepository AttendanceRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IFollowingRepository FollowingRepository { get; }
        public INotificationRepository NotificationRepository { get; }
        public IUserNotificationRepository UserNotificationRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            GigRepository = new GigRepository(context);
            AttendanceRepository = new AttendanceRepository(context);
            GenreRepository = new GenreRepository(context);
            FollowingRepository = new FollowingRepository(context);
            NotificationRepository = new NotificationRepository(context);
            UserNotificationRepository = new UserNotificationRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}