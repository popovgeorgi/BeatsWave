namespace BeatsWave.Services.Data
{
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FollowService : IFollowService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRepository<FollowInfo> followRepository;

        public FollowService(IDeletableEntityRepository<ApplicationUser> userRepository, IRepository<FollowInfo> followRepository)
        {
            this.userRepository = userRepository;
            this.followRepository = followRepository;
        }

        public async Task Create(string id)
        {
            var element = new FollowInfo
            {
                UserId = id,
            };

            await this.followRepository.AddAsync(element);
        }

        public bool Follow(string followedUserId, string followingUserId)
        {
            var followedUserInfo = this.followRepository
                .All()
                .FirstOrDefault(x => x.UserId == followedUserId);

            var followedUser = this.userRepository
                .All()
                .FirstOrDefault(x => x.Id == followedUserId);

            var followingUserInfo = this.followRepository
                .All()
                .FirstOrDefault(x => x.UserId == followingUserId);

            var followingUser = this.userRepository
                .All()
                .FirstOrDefault(x => x.Id == followingUserId);

            var isFollowed = false;

            if (followedUserInfo.Followers.Contains(followingUser))
            {
                isFollowed = false;

                followedUserInfo.Followers.Remove(followingUser);
                followingUserInfo.Following.Remove(followedUser);
            }
            else
            {
                isFollowed = true;

                followedUserInfo.Followers.Add(followingUser);
                followingUserInfo.Following.Add(followedUser);
            }

            return isFollowed;
        }
    }
}
