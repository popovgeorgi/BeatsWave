using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeatsWave.Services.Data.CloudinaryWav
{
    public interface IBeatsUploadCloudService
    {
        Task<int> UploadBeatAsync(string userId, IFormFile beatFile);
    }
}
