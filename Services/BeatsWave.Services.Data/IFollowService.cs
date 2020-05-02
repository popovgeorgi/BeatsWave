namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFollowService
    {
        bool Follow(string followedUserId, string followingUserId);

        Task Create(string id);
    }
}
