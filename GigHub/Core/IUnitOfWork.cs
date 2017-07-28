using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository GigRepository { get; }
        IAttendanceRepository AttendanceRepository { get; }
        IGenreRepository GenreRepository { get; }
        IFollowingRepository FollowingRepository { get; }
        INotificationRepository NotificationRepository { get; }
        IUserNotificationRepository UserNotificationRepository { get; }
        void Complete();
    }
}