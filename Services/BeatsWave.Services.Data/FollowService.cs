namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Common;
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;

    public class FollowService : IFollowService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Follower> followsRepository;
        private readonly INotificationsService notificationsService;

        public FollowService(IDeletableEntityRepository<ApplicationUser> usersRepository, IDeletableEntityRepository<Follower> followsRepository, INotificationsService notificationsService)
        {
            this.usersRepository = usersRepository;
            this.followsRepository = followsRepository;
            this.notificationsService = notificationsService;
        }

        public async Task<bool> FollowAsync(string followedUserId, string followingUserId)
        {
            var followedUser = this.usersRepository
                .All()
                .FirstOrDefault(x => x.Id == followedUserId);

            var followingUser = this.usersRepository
                .All()
                .FirstOrDefault(x => x.Id == followingUserId);


            bool isFollowed = false;

            var isUserAlreadyFollower = this.followsRepository.All().Where(x => x.User == followedUser).Any(x => x.FollowedBy == followingUser && x.FollowerType == FollowerType.Follower);

            if (!isUserAlreadyFollower)
            {
                isFollowed = true;

                var follower = new Follower
                {
                    FollowerType = FollowerType.Follower,
                    UserId = followedUserId,
                    FollowedById = followingUserId,
                };

                await this.followsRepository.AddAsync(follower);
                await this.followsRepository.SaveChangesAsync();
            }
            else
            {
                isFollowed = false;

                var follow = this.followsRepository.All().FirstOrDefault(x => x.User == followedUser && x.FollowedBy == followingUser && x.FollowerType == FollowerType.Follower);

                this.followsRepository.Delete(follow);
                await this.followsRepository.SaveChangesAsync();
            }

            if (isFollowed == true)
            {
                await this.notificationsService.SendNotificationAsync(followedUserId, string.Format(GlobalConstants.FollowNotification, followingUser.UserName), "Follow");
            }

            return isFollowed;
        }

        public bool IsFollowed(string followedUserId, string followingUserId)
        {
            var followedUser = this.usersRepository
                .All()
                .FirstOrDefault(x => x.Id == followedUserId);

            var followingUser = this.usersRepository
                .All()
                .FirstOrDefault(x => x.Id == followingUserId);

            var isUserAlreadyFollower = this.followsRepository.All().Where(x => x.User == followedUser).Any(x => x.FollowedBy == followingUser && x.FollowerType == FollowerType.Follower);

            return isUserAlreadyFollower;
        }
    }
}
