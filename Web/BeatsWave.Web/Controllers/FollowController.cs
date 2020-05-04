namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Follow;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class FollowController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFollowService followService;

        public FollowController(UserManager<ApplicationUser> userManager, IFollowService followService)
        {
            this.userManager = userManager;
            this.followService = followService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<FollowResponseModel>> FollowAsync(FollowInputModel inputModel)
        {
            var followingUserId = this.userManager.GetUserId(this.User);
            bool isFollowed = await this.followService.FollowAsync(inputModel.FollowedUserId, followingUserId);
            return new FollowResponseModel { IsFollowed = isFollowed };
        }
    }
}
