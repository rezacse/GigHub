using FluentAssertions;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Persistence.Repositories;
using GigHub.Test.Extentions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data.Entity;

namespace GigHub.Test.Persistance.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {

        private string _userId;
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;

        [TestInitialize]
        public void TestInitialize()
        {
            _userId = "1";
            var mockContext = new Mock<IApplicationDbContext>();

            _mockGigs = new Mock<DbSet<Gig>>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Now.AddDays(-1),
                ArtistId = _userId
            };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = _userId
            };

            gig.Cancel();

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);

            gigs.Should().BeEmpty();

        }

        [TestMethod]
        public void GetUpcomingGigsByArtist_GigIsForADifferentArtist_ShouldNotBeReturned()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = _userId
            };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId + "_");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpComingGigsByArtist_GigIsForGivenArtistAndIsInFuture_ShouldBeReturned()
        {
            var gig = new Gig
            {
                DateTime = DateTime.Now.AddDays(1),
                ArtistId = _userId
            };

            _mockGigs.SetSource(new[] { gig });

            var gigs = _repository.GetUpcomingGigsByArtist(gig.ArtistId);

            gigs.Should().Contain(gig);
        }
    }
}
