namespace BeatsWave.Web.Hubs
{
    using System.Threading.Tasks;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.Infrastructure;
    using Microsoft.AspNetCore.SignalR;
    

    public class LikeHub : Hub
    {
        private readonly ILikeService likeService;

        public LikeHub(ILikeService likeService)
        {
            this.likeService = likeService;
        }

        public async Task GetUpdateForLike(int beatId, string userId)
        {
            await this.likeService.VoteAsync(beatId, userId);
            int likes = this.likeService.GetLikes(beatId);
            await this.Clients.Caller.SendAsync("ReceiveLikeUpdate", likes, beatId, userId);
        }
    }
}
