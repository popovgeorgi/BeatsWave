namespace BeatsWave.Services.Data.CloudinaryWav
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Common;
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    public class CloudBeatService : ICloudBeatService
    {
        private readonly IConfiguration configuration;
        private Cloudinary cloudinary;

        public CloudBeatService(IConfiguration configuration)
        {
            this.configuration = configuration;

            this.InitializeCloudinary();
        }

        public async Task DeleteBeats(params string[] publicIds)
        {
            var delParams = new DelResParams
            {
                PublicIds = publicIds.ToList(),
                Invalidate = true,
            };

            await this.cloudinary.DeleteResourcesAsync(delParams);
        }

        public string GetBeatThumbnailUrl(string beatThumbnailPublicId)
        {
            var beatUrl = this.cloudinary.Api.UrlVideoUp
                .Transform(new Transformation().Height(200).Width(200).Crop("thumb"))
                .BuildUrl(string.Format("{0}.mp3", beatThumbnailPublicId));

            return beatUrl;
        }

        public string GetBeatUrl(string beatPublicId)
        {
            var beatUrl = this.cloudinary.Api.UrlVideoUp
                .BuildUrl(string.Format("{0}.wav", beatPublicId));

            return beatUrl;
        }

        public async Task<VideoUploadResult> UploadFormFileAsync(IFormFile file)
        {
            using (var memoryStream = file.OpenReadStream())
            {
                var uploadParams = new VideoUploadParams
                {
                    File = new FileDescription(Guid.NewGuid().ToString(), memoryStream),
                    PublicId = $"{GlobalConstants.PublicPicIdPrefix}{Guid.NewGuid()}",
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
