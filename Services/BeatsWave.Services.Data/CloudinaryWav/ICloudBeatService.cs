namespace BeatsWave.Services.Data.CloudinaryWav
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public interface ICloudBeatService
    {
        Task<VideoUploadResult> UploadFormFileAsync(IFormFile file);

        Task DeleteBeats(params string[] publicIds);

        string GetBeatUrl(string imagePublicId);

        string GetBeatThumbnailUrl(string imageThumbnailPublicId);
    }
}
