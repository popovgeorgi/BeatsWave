﻿namespace BeatsWave.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeatsWave.Web.ViewModels.Beats;

    public interface IBeatsService
    {
        Task<IEnumerable<FeedBeatViewModel>> GetAllBeatsAsync(int? take = null, int skip = 0);

        Task<T> FindBeatByIdAsync<T>(int id);

        Task<int> GetCountAsync();

        Task UpdateAsync(int id, string name, int standartPrice, string description);

        Task<int> GetCloudPictureId(int id);

        Task<int> GetCloudBeatId(int id);

        Task Delete(int id);

        Task<IEnumerable<FeedBeatViewModel>> SortByPopularityAsync();

        Task<IEnumerable<FeedBeatViewModel>> SortByNewestAsync();

        Task<IEnumerable<FeedBeatViewModel>> SortByOldestAsync();

        Task<IEnumerable<FeedBeatViewModel>> SortByPriceAsync();
    }
}
