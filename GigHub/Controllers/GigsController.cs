using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        //public ActionResult Mine()
        public ViewResult Mine()
        {
            var gigs = _unitOfWork.GigRepository.GetUpcomingGigsByArtist(User.Identity.GetUserId());

            return View(gigs);
        }

        [HttpPost]
        public ActionResult Search(GigViewModel gigViewModel)
        {
            return RedirectToAction("Index", "Home", new { query = gigViewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Heading = "Add a Gig",
                Genres = _unitOfWork.GenreRepository.GetGenres()
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                Venue = viewModel.Venue,
                ArtistId = User.Identity.GetUserId(),
                GenreId = viewModel.Genre,
                DateTime = viewModel.GetDateTime()
            };

            _unitOfWork.GigRepository.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine");
        }

        [Authorize]
        public ActionResult Edit(int gigId)
        {
            var gig = _unitOfWork.GigRepository.GetGig(gigId);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();


            var viewModel = new GigFormViewModel
            {
                Id = gigId,
                Heading = "Edit a Gig",
                Genres = _unitOfWork.GenreRepository.GetGenres(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.GenreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.GigRepository.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.Venue, viewModel.GetDateTime(), viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine");
        }

        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.GigRepository.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var viewModel = new GigDetailsViewModel { Gig = gig };
            if (!User.Identity.IsAuthenticated)
                return View("Details", viewModel);

            var followeeId = User.Identity.GetUserId();

            viewModel.IsAttending = _unitOfWork.AttendanceRepository.GetAttendance(gig.Id, followeeId) != null;

            viewModel.IsFollowing = _unitOfWork.FollowingRepository.GetFollowing(followeeId, gig.ArtistId) != null;

            return View("Details", viewModel);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var loginUserId = User.Identity.GetUserId();


            var viewModel = new GigViewModel
            {
                UpcomingGigs = _unitOfWork.GigRepository.GetGigsUserAttending(loginUserId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm attending",
                Attendances = _unitOfWork.AttendanceRepository.GetFutureAttendances(loginUserId).ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }


    }
}