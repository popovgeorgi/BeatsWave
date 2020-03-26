namespace BeatsWave.Services.Data
{
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public interface ICloudImageService
    {
        Task<ImageUploadResult> UploadFormFileAsync(IFormFile file);

        Task DeleteImages(params string[] publicIds);

        string GetImageUrl(string imagePublicId);

        string GetImageThumbnailUrl(string imageThumbnailPublicId);
    }
}
