namespace BeatsWave.Web.ViewModels.Notification
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class UserNotificationViewModel : IMapFrom<Notification>
    {
        public string Message { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
