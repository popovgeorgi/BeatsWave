namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.Infrastructure;
    using BeatsWave.Web.ViewModels.Likes;
    using Microsoft.AspNetCore.Authorization;

    public class LikeService : ILikeService
    {
        private readonly IRepository<Like> likeRepository;
        private readonly IDeletableEntityRepository<Beat> beatRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public LikeService(IRepository<Like> likeRepository, IDeletableEntityRepository<Beat> beatRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.likeRepository = likeRepository;
            this.beatRepository = beatRepository;
            this.userRepository = userRepository;
        }

        public IEnumerable<FanViewModel> GetFansLikedTheBeat(int beatId)
        {


            var usersWhoLikedTheBeat = this.userRepository
                .All()
                .Where(x => x.Likes.Any(l => l.BeatId == beatId && l.Type == LikeType.UpVote))
                .Select(x => new FanViewModel
                {
                    Name = x.UserName,
                    CreatedOn = x.CreatedOn,
                })
                .ToList();

            return usersWhoLikedTheBeat;
        }

        public int GetLikes(int beatId)
        {
            var likes = this.likeRepository
                .All()
                .Where(x => x.BeatId == beatId)
                .Sum(x => (int)x.Type);

            return likes;
        }

        //public CheckResult GetUpdateOfLike(string userId, int beatId)
        //{
        //    var like = this.likeRepository
        //        .All()
        //        .FirstOrDefault(x => x.BeatId == beatId && x.UserId == userId);

        //    if (like.Type == LikeType.Neutral && currentVote == true)
        //    {
        //        var result = new CheckResult
        //        {
        //            New = true,
        //            Update = "emptyLike",
        //        };

        //        return result;
        //    }

        //    if (like.Type == LikeType.UpVote && currentVote == false)
        //    {
        //        var result = new CheckResult
        //        {
        //            New = true,
        //            Update = "colourfulLike",
        //        };

        //        return result;
        //    }

        //    return new CheckResult { New = false };
        //}
        public async Task VoteAsync(int beatId, string userId)
        {
            var like = this.likeRepository
                .All()
                .FirstOrDefault(x => x.BeatId == beatId && x.UserId == userId);

            if (like != null)
            {
                var likeType = like.Type;

                if (likeType == LikeType.UpVote)
                {
                    like.Type = LikeType.Neutral;
                }
                else
                {
                    like.Type = LikeType.UpVote;
                }
            }
            else
            {
                like = new Like
                {
                    BeatId = beatId,
                    UserId = userId,
                    Type = LikeType.UpVote,
                };

                await this.likeRepository.AddAsync(like);
            }

            await this.likeRepository.SaveChangesAsync();
        }
    }
}
