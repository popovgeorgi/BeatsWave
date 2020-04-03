namespace BeatsWave.Services.Data
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

        public BeatsService(IDeletableEntityRepository<Beat> beatRepository)
        {
            this.beatRepository = beatRepository;
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

        public async Task UpdateAsync(int id, string name, decimal standartPrice, string description)
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
    }
}
