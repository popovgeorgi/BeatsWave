namespace BeatsWave.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Common.Models;

    public class Cart : BaseModel<int>
    {
        public Cart()
        {
            this.Beats = new HashSet<Beat>();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<Beat> Beats { get; set; }
    }
}
