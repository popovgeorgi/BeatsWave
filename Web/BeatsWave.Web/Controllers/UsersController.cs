namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BeatsWave.Services.Data;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Home;
    using BeatsWave.Web.ViewModels.Users;
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

        public IActionResult Buy(int id)
        {
            var beatViewModel = this.beatsService.FindBeatById<BeatToBuyViewModel>(id);

            if (beatViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(beatViewModel);
        }
    }
}