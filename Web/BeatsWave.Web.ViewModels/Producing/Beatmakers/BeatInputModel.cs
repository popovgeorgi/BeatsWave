namespace BeatsWave.Web.ViewModels.Producing.Beatmakers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using BeatsWave.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class BeatInputModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        [Display(Name = "Beat")]
        public IFormFile BeatWav { get; set; }

        [Required]
        public decimal StandartPrice { get; set; }

        [Required]
        [Range(1, 300)]
        public int Bpm { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }
    }
}
