namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Beats;
    using Microsoft.AspNetCore.Mvc;

    public class BeatsController : Controller
    {
        private readonly IBeatsService beatsService;

        public BeatsController(IBeatsService beatsService)
        {
            this.beatsService = beatsService;
        }

        public async Task<IActionResult> ByNameAsync(int id)
        {
            var beatViewModel = await this.beatsService.FindBeatByIdAsync<IndexBeatViewModel>(id);

            return this.View(beatViewModel);
        }
    }
}
