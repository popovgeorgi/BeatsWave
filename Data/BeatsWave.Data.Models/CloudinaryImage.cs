namespace BeatsWave.Data.Models
{
    using BeatsWave.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CloudinaryImage : BaseModel<int>
    {
        public string PicturePublicId { get; set; }

        public string PictureUrl { get; set; }

        public string PictureThumbnailUrl { get; set; }

        public long Length { get; set; }

        public string UploaderId { get; set; }

        public ApplicationUser Uploader { get; set; }
    }
}
