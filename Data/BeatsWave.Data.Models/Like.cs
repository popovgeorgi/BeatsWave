namespace BeatsWave.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using BeatsWave.Data.Common.Models;

    public class Like : BaseModel<int>
    {
        public int BeatId { get; set; }

        public virtual Beat Beat { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public LikeType Type { get; set; }
    }
}
