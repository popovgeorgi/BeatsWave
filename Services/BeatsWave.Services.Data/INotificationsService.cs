namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface INotificationsService
    {
        Task SendNotificationAsync(string senderId, string targetId, string message, string type);
    }
}
