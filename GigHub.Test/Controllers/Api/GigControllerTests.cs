using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Test.Extentions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Test.Controllers.Api
{
    /// <summary>
    /// Summary description for GigControllerTests
    /// </summary>
    [TestClass]
    public class GigControllerTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;


        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();

            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.GigRepository).Returns(_mockRepository.Object);

            _controller = new GigsController(mockUoW.Object);
            _userId = "1";
            _controller.MockCurrentUser(_userId, "reza@gmail.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();

        }

        [TestMethod]
        public void Cancel_GigIsCancel_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();

        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUserGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig { ArtistId = _userId + "-" };

            _mockRepository.Setup(r => r.GetGigWithAttendees(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig { ArtistId = _userId };

            _mockRepository.Setup(r => r.GetGigWithAttendees(10)).Returns(gig);

            var result = _controller.Cancel(10);

            result.Should().BeOfType<OkResult>();
        }
    }
}
