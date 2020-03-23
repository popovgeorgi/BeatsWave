namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudImageService : ICloudImageService
    {
        private readonly Cloudinary cloudinary;

        public CloudImageService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
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
            byte[] destinationImage;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, destinationStream),
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);

                return uploadResult;
            }
        }
    }
}
