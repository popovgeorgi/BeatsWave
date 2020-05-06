namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;

    public class NotificationsService : INotificationsService
    {
        private readonly IRepository<Notification> notificationRepository;

        public NotificationsService(IRepository<Notification> notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public async Task SendNotificationAsync(string senderId, string targetId, string message, string type)
        {
            if (type == "Like")
            {
                if (this.notificationRepository.All().Any(x => x.SenderId == senderId & x.UserId == targetId) == false)
                {
                    await this.notificationRepository.AddAsync(new Notification
                    {
                        SenderId = senderId,
                        UserId = targetId,
                        Message = message,
                        Type = NotificationType.Like,
                        IsSeen = false,
                    });
                }
            }
            else if (type == "Follow")
            {
                await this.notificationRepository.AddAsync(new Notification
                {
                    SenderId = senderId,
                    UserId = targetId,
                    Message = message,
                    Type = NotificationType.Follow,
                    IsSeen = false,
                });
            }
            else if (type == "Comment")
            {
                await this.notificationRepository.AddAsync(new Notification
                {
                    SenderId = senderId,
                    UserId = targetId,
                    Message = message,
                    Type = NotificationType.Comment,
                    IsSeen = false,
                });
            }

            await this.notificationRepository.SaveChangesAsync();
        }
    }
}
