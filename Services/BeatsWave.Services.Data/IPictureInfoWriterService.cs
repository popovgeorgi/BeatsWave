namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPictureInfoWriterService
    {
        Task<int> WriteToDbAsync(string uploaderId, string pictureUrl, string pictureThumbnailUrl,
            string picPublicId, DateTime uploadedOn, long picLength);
    }
}
