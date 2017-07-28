using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var userId = User.Identity.GetUserId();

            if (_unitOfWork.FollowingRepository.GetFollowing(followingDto.FolloweeId, userId) != null)
                return BadRequest("Following already exists");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followingDto.FolloweeId
            };

            _unitOfWork.FollowingRepository.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var userId = User.Identity.GetUserId();
            var following = _unitOfWork.FollowingRepository.GetFollowing(id, userId);

            if (following == null)
                return NotFound();

            _unitOfWork.FollowingRepository.Remove(following);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
