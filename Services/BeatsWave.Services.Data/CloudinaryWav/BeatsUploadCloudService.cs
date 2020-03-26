using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeatsWave.Services.Data.CloudinaryWav
{
    public class BeatsUploadCloudService : IBeatsUploadCloudService
    {
        private readonly ICloudBeatService cloudService;
        private readonly IBeatInfoWriterService pictureInfoWriter;

        public BeatsUploadCloudService(ICloudBeatService cloudinary, IBeatInfoWriterService pictureInfoWriter)
        {
            this.cloudService = cloudinary;
            this.pictureInfoWriter = pictureInfoWriter;
        }

        public async Task<int> UploadBeatAsync(string userId, IFormFile beatFile)
        {
            var uploadResult = await this.cloudService.UploadFormFileAsync(beatFile);

            var cloudinaryBeatUrl = this.cloudService.GetBeatUrl(uploadResult.PublicId);

            var cloudinaryThumbnailBeatUrl = this.cloudService.GetBeatThumbnailUrl(uploadResult.PublicId);

            var beatId = await this.pictureInfoWriter.WriteToDbAsync(userId, cloudinaryBeatUrl, cloudinaryThumbnailBeatUrl, uploadResult.PublicId, uploadResult.CreatedAt, uploadResult.Length);

            return beatId;
        }
    }
}
