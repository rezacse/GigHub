﻿using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string userId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == userId &&
                            !g.IsCanceled &&
                            g.DateTime > DateTime.Now)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetGigsUserAttending(string loginUserId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == loginUserId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .ToList();
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }

        public IEnumerable<Gig> GetUpcomingGigs()
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now && g.IsCanceled == false);
        }
    }
}