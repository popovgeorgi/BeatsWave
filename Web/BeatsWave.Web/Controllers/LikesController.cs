namespace BeatsWave.Web.Controllers
{
    using System.Threading.Tasks;

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

        public LikesController(ILikeService likeService, UserManager<ApplicationUser> userManager)
        {
            this.likeService = likeService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<LikesResponseModel>> Beat(LikeInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.likeService.VoteAsync(input.BeatId, userId);
            var likes = this.likeService.GetLikes(input.BeatId);
            return new LikesResponseModel { LikesCount = likes };
        }
    }
}
