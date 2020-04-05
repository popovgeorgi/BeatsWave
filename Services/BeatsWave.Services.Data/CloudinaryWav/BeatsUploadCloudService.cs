using BeatsWave.Data.Common.Repositories;
using BeatsWave.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatsWave.Services.Data.CloudinaryWav
{
    public class BeatsUploadCloudService : IBeatsUploadCloudService
    {
        private readonly ICloudBeatService cloudService;
        private readonly IBeatInfoWriterService pictureInfoWriter;
        private readonly IDeletableEntityRepository<CloudinaryBeat> beatRepository;

        public BeatsUploadCloudService(ICloudBeatService cloudinary, IBeatInfoWriterService pictureInfoWriter, IDeletableEntityRepository<CloudinaryBeat> beatRepository)
        {
            this.cloudService = cloudinary;
            this.pictureInfoWriter = pictureInfoWriter;
            this.beatRepository = beatRepository;
        }

        public async Task DeleteBeatAsync(int beatId)
        {
            var beatFromDb = await this.beatRepository
                .All()
                .Where(x => x.Id == beatId)
                .FirstOrDefaultAsync();

            var beatPublicId = beatFromDb.BeatPublicId;

            this.beatRepository.Delete(beatFromDb);

            await this.beatRepository.SaveChangesAsync();

            await this.cloudService.DeleteBeats(beatPublicId);
        }

        public async Task<int> UploadBeatAsync(string userId, IFormFile beatFile)
        {
            var uploadResult = await this.cloudService.UploadFormFileAsync(beatFile);

            var cloudinaryBeatUrl = this.cloudService.GetBeatUrl(uploadResult.PublicId);

            var cloudinaryThumbnailBeatUrl = this.cloudService.GetBeatThumbnailUrl(uploadResult.PublicId);

            var beatId = await this.pictureInfoWriter.WriteToDbAsync(userId, cloudinaryBeatUrl, cloudinaryThumbnailBeatUrl, uploadResult.PublicId, uploadResult.CreatedAt, uploadResult.Length);

            return beatId;
        }
    }
}
