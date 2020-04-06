namespace BeatsWave.Services.Data
{
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class LikeService : ILikeService
    {
        private readonly IRepository<Like> likeRepository;

        public LikeService(IRepository<Like> likeRepository)
        {
            this.likeRepository = likeRepository;
        }

        public int GetLikes(int beatId)
        {
            var likes = this.likeRepository
                .All()
                .Where(x => x.BeatId == beatId)
                .Sum(x => (int)x.Type);

            return likes;
        }

        public async Task VoteAsync(int beatId, string userId, bool isUpVote)
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
