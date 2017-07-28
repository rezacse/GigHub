using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string followeeId, string followerId);
        IEnumerable<Following> GetFollowingByFollowerId(string followerId);
        void Add(Following following);
        void Remove(Following following);
    }
}