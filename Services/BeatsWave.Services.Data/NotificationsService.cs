namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Notification;

    public class NotificationsService : INotificationsService
    {
        private readonly IRepository<Notification> notificationRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public NotificationsService(IRepository<Notification> notificationRepository, IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.notificationRepository = notificationRepository;
            this.usersRepository = usersRepository;
        }

        public T GetAllNotifications<T>(string userId)
        {
            var userNotifications = this.usersRepository
                .All()
                .Where(x => x.Id == userId)
                .To<T>()
                .FirstOrDefault();

            return userNotifications;
        }

        public int GetUnseenCount(string userId)
        {
            var count = this.notificationRepository
                .All()
                .Where(x => x.UserId == userId && x.IsSeen == false)
                .Count();

            return count;
        }

        public async Task MakeAllNotificationsRead(string userId)
        {
            var userNotifications = this.notificationRepository
                .All()
                .Where(x => x.UserId == userId && x.IsSeen == false)
                .ToList();

            foreach (var notificaton in userNotifications)
            {
                notificaton.IsSeen = true;
            }

            await this.usersRepository.SaveChangesAsync();
        }

        public async Task SendNotificationAsync(string targetId, string message, string type)
        {
            if (type == "Like")
            {
                await this.notificationRepository.AddAsync(new Notification
                {
                    UserId = targetId,
                    Message = message,
                    Type = NotificationType.Like,
                    IsSeen = false,
                });
            }
            else if (type == "Follow")
            {
                await this.notificationRepository.AddAsync(new Notification
                {
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
