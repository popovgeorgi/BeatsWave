namespace BeatsWave.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Common.Models;

    public class Follower : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string FollowedById { get; set; }

        public ApplicationUser FollowedBy { get; set; }

        public FollowerType FollowerType { get; set; }
    }
}
