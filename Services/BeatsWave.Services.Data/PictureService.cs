namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using Microsoft.AspNetCore.Http;

    public class PictureService : IPictureService
    {
        private readonly ICloudImageService cloudService;
        private readonly IPictureInfoWriterService pictureInfoWriter;
        private readonly IRepository<CloudinaryImage> imageRepository;

        public PictureService(ICloudImageService cloudinary, IPictureInfoWriterService pictureInfoWriter)
        {
            this.cloudService = cloudinary;
            this.pictureInfoWriter = pictureInfoWriter;
        }

        public async Task<int> UploadImageAsync(string userId, IFormFile pictureFile)
        {
            var uploadResult = await this.cloudService.UploadFormFileAsync(pictureFile);

            var cloudinaryPictureUrl = this.cloudService.GetImageUrl(uploadResult.PublicId);

            var cloudinaryThumbnailPictureUrl = this.cloudService.GetImageThumbnailUrl(uploadResult.PublicId);

            var pictureId = await this.pictureInfoWriter.WriteToDbAsync(userId, cloudinaryPictureUrl, cloudinaryThumbnailPictureUrl, uploadResult.PublicId, uploadResult.CreatedAt, uploadResult.Length);

            return pictureId;
        }
    }
}
