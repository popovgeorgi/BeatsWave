namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILikeService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="beatId"></param>
        /// <param name="userId"></param>
        /// <param name="isUpVote">If true - up vote, else - neutral. </param>
        /// <returns></returns>
        Task VoteAsync(int beatId, string userId, bool isUpVote);

        int GetLikes(int beatId);
    }
}
