namespace BeatsWave.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Common.Models;

    public class CloudinaryBeat : BaseDeletableModel<int>
    {
        public string BeatPublicId { get; set; }

        public string BeatUrl { get; set; }

        public string BeatThumbnailUrl { get; set; }

        public long Length { get; set; }

        public string UploaderId { get; set; }

        public ApplicationUser Uploader { get; set; }
    }
}
