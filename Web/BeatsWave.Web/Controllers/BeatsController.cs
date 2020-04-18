namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Beats;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BeatsController : Controller
    {
        private readonly IBeatsService beatsService;
        private readonly ILikeService likeService;
        private readonly UserManager<ApplicationUser> userManager;

        public BeatsController(IBeatsService beatsService, ILikeService likeService, UserManager<ApplicationUser> userManager)
        {
            this.beatsService = beatsService;
            this.likeService = likeService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ByNameAsync(int id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var beatViewModel = await this.beatsService.FindBeatByIdAsync<IndexBeatViewModel>(id);
            var usersWhoLikedTheBeat = this.likeService.GetFansLikedTheBeat(id);
            var isLikedByThisUser = this.likeService.IsLikedByCurrentUser(userId, id);

            var viewModel = new ByNameViewModel();
            viewModel.Beat = beatViewModel;
            viewModel.Fans = usersWhoLikedTheBeat;
            viewModel.IsLikedByCurrentUser = isLikedByThisUser;

            return this.View(viewModel);
        }
    }
}
