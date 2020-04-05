namespace BeatsWave.Services.Data
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IPictureService
    {
        Task<int> UploadImageAsync(string userId, IFormFile pictureFile);

        Task DeleteImageAsync(int pictureId);
    }
}
