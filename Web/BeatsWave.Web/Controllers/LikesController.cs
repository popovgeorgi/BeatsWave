namespace BeatsWave.Web.Controllers
{
    using System.Threading.Tasks;

    using BeatsWave.Common;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Likes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : ControllerBase
    {
        private readonly ILikeService likeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationsService notificationsService;
        private readonly IBeatsService beatsService;

        public LikesController(ILikeService likeService, UserManager<ApplicationUser> userManager, INotificationsService notificationsService, IBeatsService beatsService)
        {
            this.likeService = likeService;
            this.userManager = userManager;
            this.notificationsService = notificationsService;
            this.beatsService = beatsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LikesResponseModel>> Beat(LikeInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            bool isLiked = await this.likeService.VoteAsync(input.BeatId, userId);
            var ownerId = this.beatsService.FindUserIdByBeatId(input.BeatId);
            var beatName = this.beatsService.GetBeatNameByBeatId(input.BeatId);
            if (isLiked == true)
            {
                await this.notificationsService.SendNotificationAsync(ownerId, string.Format(GlobalConstants.LikeNotification, $"{this.User.Identity.Name}", $"{beatName}"), "Like");
            }

            var likes = this.likeService.GetLikes(input.BeatId);
            return new LikesResponseModel { LikesCount = likes, IsLiked = isLiked };
        }
    }
}
