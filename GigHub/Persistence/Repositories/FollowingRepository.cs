﻿using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Following GetFollowing(string followeeId, string followerId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FolloweeId == followeeId && f.FollowerId == followerId);
        }

        public IEnumerable<Following> GetFollowingByFollowerId(string followerId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == followerId)
                .ToList();
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}