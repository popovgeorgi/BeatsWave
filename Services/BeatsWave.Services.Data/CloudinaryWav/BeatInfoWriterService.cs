namespace BeatsWave.Services.Data.CloudinaryWav
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;

    public class BeatInfoWriterService : IBeatInfoWriterService
    {
        private readonly IDeletableEntityRepository<CloudinaryBeat> cloudBeatsRepository;

        public BeatInfoWriterService(IDeletableEntityRepository<CloudinaryBeat> cloudBeatsRepository)
        {
            this.cloudBeatsRepository = cloudBeatsRepository;
        }
        public async Task<int> WriteToDbAsync(string uploaderId, string beatUrl, string beatThumbnailUrl,
            string picPublicId, DateTime uploadedOn, long picLength)
        {
            var cloudBeat = new CloudinaryBeat
            {
                UploaderId = uploaderId,
                BeatUrl = beatUrl,
                BeatThumbnailUrl = beatThumbnailUrl,
                BeatPublicId = picPublicId,
                CreatedOn = uploadedOn,
                Length = picLength,
            };

            await this.cloudBeatsRepository.AddAsync(cloudBeat);

            await this.cloudBeatsRepository.SaveChangesAsync();

            return cloudBeat.Id;
        }
    }
}
