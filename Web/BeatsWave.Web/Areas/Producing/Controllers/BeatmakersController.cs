namespace BeatsWave.Web.Areas.Producing.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Common;
    using BeatsWave.Web.ViewModels.Producing.Beatmakers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using CloudinaryDotNet;
    using BeatsWave.Services.Data;
    using Microsoft.AspNetCore.Identity;
    using BeatsWave.Data.Models;

    [Authorize(Roles = GlobalConstants.BeatmakerRoleName)]
    [Area("Producing")]
    public class BeatmakersController : Controller
    {
        private readonly Cloudinary cloudinary;
        private readonly IProducersService producersService;
        private readonly UserManager<ApplicationUser> userManager;

        public BeatmakersController(Cloudinary cloudinary, IProducersService producersService, UserManager<ApplicationUser> userManager)
        {
            this.cloudinary = cloudinary;
            this.producersService = producersService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Upload()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(BeatInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var userId = this.userManager.GetUserId(this.User);

            await this.producersService.CreateBeatAsync(inputModel.Name, inputModel.Image, inputModel.BeatWav,
                inputModel.StandartPrice, inputModel.Bpm, inputModel.Genre.ToString(), inputModel.Description, userId);

            return this.RedirectToAction("All", "Home", new { area = " "});
        }
    }
}
