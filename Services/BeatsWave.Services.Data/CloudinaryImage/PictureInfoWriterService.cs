namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;

    public class PictureInfoWriterService : IPictureInfoWriterService
    {
        private readonly IRepository<CloudinaryImage> imageRepository;

        public PictureInfoWriterService(IRepository<CloudinaryImage> imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        public async Task<int> WriteToDbAsync(string uploaderId, string pictureUrl, string pictureThumbnailUrl,
            string picPublicId, DateTime uploadedOn, long picLength)
        {
            var image = new CloudinaryImage
            {
                UploaderId = uploaderId,
                PictureUrl = pictureUrl,
                PictureThumbnailUrl = pictureThumbnailUrl,
                PicturePublicId = picPublicId,
                CreatedOn = uploadedOn,
                Length = picLength,
            };

            await this.imageRepository.AddAsync(image);

            await this.imageRepository.SaveChangesAsync();

            return image.Id;
        }

        public async Task<int> WriteToProfileDbAsync(string uploaderId, string pictureUrl, string pictureThumbnailUrl,
            string picPublicId, DateTime uploadedOn, long picLength, string profilePictureUploaderId)
        {
            var image = new CloudinaryImage
            {
                UploaderId = uploaderId,
                PictureUrl = pictureUrl,
                PictureThumbnailUrl = pictureThumbnailUrl,
                PicturePublicId = picPublicId,
                CreatedOn = uploadedOn,
                Length = picLength,
                ProfilePictureUploaderId = profilePictureUploaderId,
            };

            await this.imageRepository.AddAsync(image);

            await this.imageRepository.SaveChangesAsync();

            return image.Id;
        }
    }
}
