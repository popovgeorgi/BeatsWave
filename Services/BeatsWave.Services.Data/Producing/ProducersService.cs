namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data.CloudinaryWav;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProducersService : IProducersService
    {
        private readonly IDeletableEntityRepository<Beat> beatsRepository;
        private readonly IPictureService pictureService;
        private readonly IBeatsUploadCloudService beatsService;

        public ProducersService(IDeletableEntityRepository<Beat> beatsRepository, IPictureService pictureService, IBeatsUploadCloudService beatsService)
        {
            this.beatsRepository = beatsRepository;
            this.pictureService = pictureService;
            this.beatsService = beatsService;
        }

        public async Task<int> CreateBeatAsync(string name, IFormFile image, IFormFile beat, decimal price, int bpm, string genre, string description, string userId)
        {
            var imageId = await this.pictureService.UploadImageAsync(userId, image);
            var beatUrlId = await this.beatsService.UploadBeatAsync(userId, beat);

            var outcome = new Beat
            {
                Name = name,
                StandartPrice = price,
                Bpm = bpm,
                Genre = (Genre)Enum.Parse(typeof(Genre), genre),
                Description = description,
                CloudinaryImageId = imageId,
                CloudinaryBeatId = beatUrlId,
                ProducerId = userId,
            };

            await this.beatsRepository.AddAsync(outcome);

            await this.beatsRepository.SaveChangesAsync();

            return outcome.Id;
        }
    }
}
