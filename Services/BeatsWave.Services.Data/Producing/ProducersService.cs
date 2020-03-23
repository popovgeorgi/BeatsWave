namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class ProducersService : IProducersService
    {
        private readonly IDeletableEntityRepository<Beat> beatsRepository;
        private readonly IPictureService pictureService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProducersService(IDeletableEntityRepository<Beat> beatsRepository, IPictureService pictureService)
        {
            this.beatsRepository = beatsRepository;
            this.pictureService = pictureService;
        }

        public async Task<int> CreateBeatAsync(string name, IFormFile image, IFormFile beat, decimal price, int bpm, string genre, string description, string userId)
        {
            var imageId = await this.pictureService.UploadImageAsync(userId, image);

            var outcome = new Beat
            {
                Name = name,
                StandartPrice = price,
                Bpm = bpm,
                Genre = (Genre)Enum.Parse(typeof(Genre), genre),
                Description = description,
                ImageUrl = imageId,
                ProducerId = userId,
            };

            await this.beatsRepository.AddAsync(outcome);

            await this.beatsRepository.SaveChangesAsync();

            return outcome.Id;
        }
    }
}
