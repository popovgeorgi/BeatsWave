namespace BeatsWave.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using BeatsWave.Data.Common.Models;

    public class Beat : BaseDeletableModel<int>
    {
        [Required]
        public string Name { get; set; }

        //photoInCloudinary
        public string ImageUrl { get; set; }

        //beatInCloudinary
        public string BeatUrl { get; set; }
        //mp3
        public decimal StandartPrice { get; set; }

        //mp3 + wav
        public decimal? PremiumPrice { get; set; }

        //mp3 + wav + trackStems
        public decimal? TrackoutPrice { get; set; }

        public int Bpm { get; set; }

        public Genre Genre { get; set; }

        public string Description { get; set; }

        public string ProducerId { get; set; }

        public ApplicationUser Producer { get; set; }
    }
}
