namespace BeatsWave.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeatsWave.Web.ViewModels.Beats;

    public interface IBeatsService
    {
        Task<IEnumerable<T>> GetAllBeatsAsync<T>(int? take = null, int skip = 0);

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

        string FindUserIdByBeatId(int beatId);

        string GetBeatNameByBeatId(int beatId);

        Task<IEnumerable<T>> GetBeatsByGenre<T>(string genre);
    }
}
