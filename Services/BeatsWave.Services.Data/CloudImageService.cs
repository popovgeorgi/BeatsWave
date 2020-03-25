namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BeatsWave.Common;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    public class CloudImageService : ICloudImageService
    {
        private readonly IConfiguration configuration;
        private Cloudinary cloudinary;

        public CloudImageService(IConfiguration configuration)
        {
            this.configuration = configuration;

            this.InitializeCloudinary();
        }

        public async Task DeleteImages(params string[] publicIds)
        {
            var delParams = new DelResParams
            {
                PublicIds = publicIds.ToList(),
                Invalidate = true,
            };

            await this.cloudinary.DeleteResourcesAsync(delParams);
        }

        public string GetImageThumbnailUrl(string imagePublicId)
        {
            var pictureUrl = this.cloudinary.Api.UrlImgUp
                .Transform(new Transformation().Height(200).Width(200).Crop("thumb"))
                .BuildUrl(string.Format("{0}.jpg", imagePublicId));

            return pictureUrl;
        }

        public string GetImageUrl(string imagePublicId)
        {
            var pictureUrl = this.cloudinary.Api.UrlImgUp
                .BuildUrl(string.Format("{0}.jpg", imagePublicId));

            return pictureUrl;
        }

        public async Task<ImageUploadResult> UploadFormFileAsync(IFormFile file)
        {
            using (var memoryStream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), memoryStream),
                    PublicId = $"{GlobalConstants.PublicPicIdPrefix}{Guid.NewGuid()}",
                    Transformation = new Transformation().Crop(GlobalConstants.CropLimit).Width(800).Height(600),
                    EagerTransforms = new List<Transformation>
                    {
                        new Transformation().Width(GlobalConstants.ThumbnailWidth).Height(GlobalConstants.ThumbnailHeight).Crop(GlobalConstants.CropThumb),
                    },
                };

                var uploadResult = await this.cloudinary.UploadAsync(uploadParams);

                return uploadResult;
            }
        }

        private void InitializeCloudinary()
        {

            var key = this.configuration.GetSection("Cloudinary:CloudName").Value;
            this.cloudinary = new Cloudinary(
                new Account(
                    this.configuration.GetSection("Cloudinary:CloudName").Value,
                    this.configuration.GetSection("Cloudinary:ApiKey").Value,
                    this.configuration.GetSection("Cloudinary:ApiSecret").Value));
        }
    }
}
