namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFollowService
    {
        Task<bool> FollowAsync(string followedUserId, string followingUserId);

        bool IsFollowed(string followedUserId, string followingUserId);
    }
}
