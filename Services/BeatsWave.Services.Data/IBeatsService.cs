﻿namespace BeatsWave.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeatsWave.Web.ViewModels.Home;

    public interface IBeatsService
    {
        Task<IEnumerable<IndexBeatViewModel>> GetAllBeatsAsync(int? take = null, int skip = 0);

        Task<T> FindBeatByIdAsync<T>(int id);

        Task<int> GetCountAsync();

        Task UpdateAsync(int id, string name, decimal standartPrice, string description);
    }
}
