namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Beats;
    using BeatsWave.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IBeatsService beatsService;
        private readonly IFollowService followService;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(IUsersService usersService, IBeatsService beatsService, IFollowService followService, UserManager<ApplicationUser> userManager)
        {
            this.usersService = usersService;
            this.beatsService = beatsService;
            this.followService = followService;
            this.userManager = userManager;
        }

        public IActionResult Profile(string id)
        {
            var userId = this.userManager.GetUserId(this.User);

            var userViewModel = this.usersService.GetById<UserViewModel>(id);
            userViewModel.IsFollowedByCurrentUser = this.followService.IsFollowed(id, userId);

            if (userViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(userViewModel);
        }

        public IActionResult Favourites(string id)
        {
            var viewModel = this.usersService.GetLikedBeats(id);

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Buy(int id)
        {
            var beatViewModel = await this.beatsService.FindBeatByIdAsync<BeatToBuyViewModel>(id);

            if (beatViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(beatViewModel);
        }

        public IActionResult ChooseRole()
        {
            return this.View();
        }

        public IActionResult Platform(int roleId)
        {
            return this.View();
        }
    }
}