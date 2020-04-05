﻿namespace BeatsWave.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Home;
    using Microsoft.EntityFrameworkCore;

    public class BeatsService : IBeatsService
    {
        private readonly IDeletableEntityRepository<Beat> beatRepository;
        private readonly IRepository<CloudinaryImage> imageRepository;

        public BeatsService(IDeletableEntityRepository<Beat> beatRepository, IRepository<CloudinaryImage> imageRepository)
        {
            this.beatRepository = beatRepository;
            this.imageRepository = imageRepository;
        }

        public async Task<int> GetCountAsync()
        {
            var count = await this.beatRepository
                .All()
                .CountAsync();

            return count;
        }

        public async Task<T> FindBeatByIdAsync<T>(int id)
        {
            var beat = await this.beatRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return beat;
        }

        public async Task<IEnumerable<IndexBeatViewModel>> GetAllBeatsAsync(int? take = null, int skip = 0)
        {
            var beats = await this.beatRepository.All()
                .OrderByDescending(x => x.CreatedOn)
                .Skip(skip)
                .Take((int)take)
                .To<IndexBeatViewModel>()
                .ToListAsync();

            return beats;
        }

        public async Task UpdateAsync(int id, string name, int standartPrice, string description)
        {
            var beat = await this.beatRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            beat.Name = name;
            beat.StandartPrice = standartPrice;
            beat.Description = description;

            await this.beatRepository.SaveChangesAsync();
        }

        public async Task<int> GetCloudPictureId(int id)
        {
            var beat = await this.beatRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var cloudImageId = beat.CloudinaryImageId;

            return cloudImageId;
        }

        public async Task<int> GetCloudBeatId(int id)
        {
            var beat = await this.beatRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            var cloudBeatId = beat.CloudinaryBeatId;

            return cloudBeatId;
        }

        public async Task Delete(int id)
        {
            var beat = await this.beatRepository
                .All()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            this.beatRepository.Delete(beat);

            await this.beatRepository.SaveChangesAsync();
        }
    }
}
