using FluentAssertions;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence.Repositories;
using GigHub.Test.Extentions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Test.Persistance.Repositories
{
    [TestClass]
    public class NotificationRepositoryTests
    {
        private string _userId;
        private NotificationRepository _repository;
        private Mock<DbSet<UserNotification>> _mockNotifications;

        [TestInitialize]
        public void TestInitialize()
        {
            _userId = "1";

            _mockNotifications = new Mock<DbSet<UserNotification>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.UserNotifications).Returns(_mockNotifications.Object);

            _repository = new NotificationRepository(mockContext.Object);
        }


        [TestMethod]
        public void GetNewNotificationsFor_NotificationIsRead_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = _userId };
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNotificationsWithArtistAndNotificationByUserId(user.Id);

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationsFor_NotificationIsForADifferentUser_ShouldNotBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = _userId };
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNotificationsWithArtistAndNotificationByUserId(user.Id + "-");

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNewNotificationsFor_NewNotificationForTheGivenUser_ShouldBeReturned()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = _userId };
            var userNotification = new UserNotification(user, notification);

            _mockNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNotificationsWithArtistAndNotificationByUserId(user.Id);

            notifications.Should().HaveCount(1);
            notifications.First().Should().Be(notification);
        }
    }
}
