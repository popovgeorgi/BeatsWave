namespace BeatsWave.Web.ViewModels.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class UserNotificationsViewModel : IMapFrom<ApplicationUser>
    {
        public virtual ICollection<UserNotificationViewModel> Notifications { get; set; }
    }
}
