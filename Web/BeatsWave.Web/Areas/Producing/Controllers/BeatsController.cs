namespace BeatsWave.Web.Areas.Producing.Controllers
{
    using BeatsWave.Common;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Beats;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Authorize(Roles = GlobalConstants.BeatmakerRoleName)]
    [Area("Producing")]
    public class BeatsController : Controller
    {
        private readonly IBeatsService beatsService;

        public BeatsController(IBeatsService beatsService)
        {
            this.beatsService = beatsService;
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
    }
}
