namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Web.ViewModels.Notification;

    public interface INotificationsService
    {
        Task SendNotificationAsync(string targetId, string message, string type);

        int GetUnseenCount(string userId);

        T GetAllNotifications<T>(string userId);

        Task MakeAllNotificationsRead(string userId);
    }
}
