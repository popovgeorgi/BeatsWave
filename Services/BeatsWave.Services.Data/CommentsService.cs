using BeatsWave.Common;
using BeatsWave.Data.Common.Repositories;
using BeatsWave.Data.Models;
using System;
using System.Threading.Tasks;

namespace BeatsWave.Services.Data
{
    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;
        private readonly INotificationsService notificationsService;
        private readonly IBeatsService beatsService;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
            this.notificationsService = notificationsService;
            this.beatsService = beatsService;
        }

        public async Task Create(int beatId, string userId, string content)
        {
            var comment = new Comment
            {
                Content = content,
                BeatId = beatId,
                UserId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }
    }
}
