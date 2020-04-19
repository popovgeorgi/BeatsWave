namespace BeatsWave.Web.Hubs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Beats;
    using Microsoft.AspNetCore.SignalR;

    public class SortHub : Hub
    {
        private readonly IBeatsService beatsService;

        public SortHub(IBeatsService beatsService)
        {
            this.beatsService = beatsService;
        }

        public async Task SortBeats(string sortCondition)
        {
            var beats = Enumerable.Empty<FeedBeatViewModel>();

            if (sortCondition == "Popularity")
            {
                beats = await this.beatsService.SortByPopularityAsync();
            }
            else if (sortCondition == "Newest")
            {
                beats = await this.beatsService.SortByNewestAsync();
            }
            else if (sortCondition == "Oldest")
            {
                beats = await this.beatsService.SortByOldestAsync();
            }
            else if (sortCondition == "Price")
            {
                beats = await this.beatsService.SortByPriceAsync();
            }

            await this.Clients.Caller.SendAsync("ReceiveSortUpdate", beats);
        }
    }
}
