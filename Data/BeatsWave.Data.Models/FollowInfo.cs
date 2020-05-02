namespace BeatsWave.Data.Models
{
    using System.Collections.Generic;

    using BeatsWave.Data.Common.Models;

    public class FollowInfo : BaseModel<int>
    {
        public string UserId { get; set; }

        public virtual ICollection<ApplicationUser> Followers { get; set; }

        public virtual ICollection<ApplicationUser> Following { get; set; }
    }
}
