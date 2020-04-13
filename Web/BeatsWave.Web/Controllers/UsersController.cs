namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BeatsWave.Services.Data;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Beats;
    using BeatsWave.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IBeatsService beatsService;

        public UsersController(IUsersService usersService, IBeatsService beatsService)
        {
            this.usersService = usersService;
            this.beatsService = beatsService;
        }

        public IActionResult Profile(string id)
        {
            var userViewModel = this.usersService.GetById<UserViewModel>(id);

            if (userViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(userViewModel);
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