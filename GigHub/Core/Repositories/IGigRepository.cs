using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string userId);
        IEnumerable<Gig> GetGigsUserAttending(string loginUserId);
        Gig GetGigWithAttendees(int gigId);
        void Add(Gig gig);
        IEnumerable<Gig> GetUpcomingGigs();
    }
}