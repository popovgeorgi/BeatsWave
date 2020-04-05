namespace BeatsWave.Web.Areas.Producing.Controllers
{
    using BeatsWave.Common;
    using BeatsWave.Services.Data;
    using BeatsWave.Services.Data.CloudinaryWav;
    using BeatsWave.Web.ViewModels.Beats;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.BeatmakerRoleName)]
    [Area("Producing")]
    public class BeatsController : Controller
    {
        private readonly IBeatsService beatsService;
        private readonly IBeatsUploadCloudService beatsCloudService;
        private readonly IPictureService pictureCloudService;

        public BeatsController(IBeatsService beatsService, IBeatsUploadCloudService beatsCloudService, IPictureService pictureCloudService)
        {
            this.beatsService = beatsService;
            this.beatsCloudService = beatsCloudService;
            this.pictureCloudService = pictureCloudService;
        }

        public async Task<IActionResult> Edit(int id)
        {
            var beatViewModel = await this.beatsService.FindBeatByIdAsync<EditBeatViewModel>(id);

            if (beatViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(beatViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBeatViewModel viewModel)
        {
            await this.beatsService.UpdateAsync(id, viewModel.Name, viewModel.StandartPrice, viewModel.Description);

            return this.RedirectToAction("All", "Home", new { Area = " " });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cloudImageId = await this.beatsService.GetCloudPictureId(id);
            var cloudBeatId = await this.beatsService.GetCloudBeatId(id);

            await this.beatsService.Delete(id);
            await this.beatsCloudService.DeleteBeatAsync(cloudBeatId);
            await this.pictureCloudService.DeleteImageAsync(cloudImageId);

            return this.RedirectToAction("All", "Home", new { Area = " " });
        }
    }
}
