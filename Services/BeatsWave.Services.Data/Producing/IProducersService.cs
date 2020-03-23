namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface IProducersService
    {
        Task<int> CreateBeatAsync(string name, IFormFile image, IFormFile beat, decimal price, int bpm, string genre, string description, string userId);
    }
}
