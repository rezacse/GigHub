using GigHub.App_Start;
using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetNotifications()
        {
            var userId = User.Identity.GetUserId();

            var notifications = _unitOfWork.NotificationRepository
                .GetNotificationsWithArtistAndNotificationByUserId(userId)
                .ToList();

            var res = MappingProfile._mapper.Map<List<Notification>, IEnumerable<NotificationDto>>(notifications);
            return res;
            //var not = notifications.Select(Mapper.Map<Notification, NotificationDto>).ToList();
        }

        [HttpPost]
        public IHttpActionResult MarkAsResult()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork.UserNotificationRepository.GetUserNotificationsByUserId(userId);

            notifications.ForEach(n => n.Read());

            _unitOfWork.Complete();

            return Ok();
        }
    }
}
