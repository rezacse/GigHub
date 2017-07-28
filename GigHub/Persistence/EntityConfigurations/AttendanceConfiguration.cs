using GigHub.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace GigHub.Persistence.EntityConfigurations
{
    public class AttendanceConfiguration : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfiguration()
        {
            //[Key]
            //[Column(Order = 1)]  
            //[Key]
            //[Column(Order = 2)]
            HasKey(a => new { a.GigId, a.AttendeeId });
        }
    }
}