namespace BeatsWave.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BeatsWave.Data.Common.Models;

    public class Beat : BaseDeletableModel<int>
    {
        public Beat()
        {
            this.Likes = new HashSet<Like>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CloudinaryBeatId { get; set; }

        public CloudinaryBeat CloudinaryBeat { get; set; }

        [Required]
        public int CloudinaryImageId { get; set; }

        public CloudinaryImage CloudinaryImage { get; set; }

        //mp3
        public int StandartPrice { get; set; }

        //mp3 + wav
        public int? PremiumPrice { get; set; }

        //mp3 + wav + trackStems
        public int? TrackoutPrice { get; set; }

        public int Bpm { get; set; }

        public Genre Genre { get; set; }

        public string Description { get; set; }

        [Required]
        public string ProducerId { get; set; }

        public ApplicationUser Producer { get; set; }

        public virtual ICollection<Like> Likes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
