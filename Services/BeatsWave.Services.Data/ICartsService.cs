namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Data.Models;
    using BeatsWave.Web.ViewModels.Beats;
    using BeatsWave.Web.ViewModels.Checkout;

    public interface ICartsService
    {
        Task<string> AddAsync(int beatId, string userId);

        Task CreateCart(string userId);

        int Count(string userId);

        T GetCartBeats<T>(string userId);

        bool Remove(int beatId, string userId);

        int TotalPrice(string userId);

        ICollection<CheckoutBeatViewModel> GetCartBeatsSecond(string userId);
    }
}
