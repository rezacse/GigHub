using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetFutureAttendances(string loginUserId);
        Attendance GetAttendance(int gigId, string attendeeId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}