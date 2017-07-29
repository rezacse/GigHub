using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto attendanceDto)
        {
            var attendeeId = User.Identity.GetUserId();

            if (_unitOfWork.AttendanceRepository.GetAttendance(attendanceDto.GigId, attendeeId) != null)
                return BadRequest("The attendance already exists");

            var attendance = new Attendance
            {
                GigId = attendanceDto.GigId,
                AttendeeId = attendeeId
            };

            _unitOfWork.AttendanceRepository.Add(attendance);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var attendeeId = User.Identity.GetUserId();

            var attendance = _unitOfWork.AttendanceRepository.GetAttendance(id, attendeeId);

            if (attendance == null)
                return NotFound();

            _unitOfWork.AttendanceRepository.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
