namespace BeatsWave.Services.Data
{
    using BeatsWave.Web.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BeatsWave.Web.ViewModels.Likes;

    public interface ILikeService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="beatId"></param>
        /// <param name="userId"></param>
        /// <param name="isUpVote">If true - up vote, else - neutral. </param>
        /// <returns></returns>
        Task VoteAsync(int beatId, string userId);

        int GetLikes(int beatId);

        IEnumerable<FanViewModel> GetFansLikedTheBeat(int beatId);
    }
}
