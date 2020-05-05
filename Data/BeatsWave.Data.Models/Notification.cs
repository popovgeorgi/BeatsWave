namespace BeatsWave.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Common.Models;

    public class Notification : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        public string Message { get; set; }

        public NotificationType Type { get; set; }

        public bool IsSeen { get; set; }
    }
}
