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
    using BeatsWave.Web.CloudinaryHelper;
    using CloudinaryDotNet;

    [Authorize(Roles = GlobalConstants.BeatmakerRoleName)]
    [Area("Producing")]
    public class BeatmakersController : Controller
    {
        private readonly Cloudinary cloudinary;

        public BeatmakersController(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
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

            await CloudinaryExtension.UploadAsync(this.cloudinary, inputModel.Image);

            return this.Json(inputModel);
        }
    }
}
