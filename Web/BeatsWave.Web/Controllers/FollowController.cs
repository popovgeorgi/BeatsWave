namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Follow;
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

        public ActionResult<FollowResponseModel> Follow(FollowInputModel inputModel)
        {
            var followingUserId = this.userManager.GetUserId(this.User);
            bool isFollowed = this.followService.Follow(inputModel.FollowedUserId, followingUserId);
            return new FollowResponseModel { IsFollowed = isFollowed };
        }
    }
}
