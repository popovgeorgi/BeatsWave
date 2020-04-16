namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Beats;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class BeatsController : Controller
    {
        private readonly IBeatsService beatsService;
        private readonly ILikeService likeService;

        public BeatsController(IBeatsService beatsService, ILikeService likeService)
        {
            this.beatsService = beatsService;
            this.likeService = likeService;
        }
        public async Task<IActionResult> ByNameAsync(int id)
        {
            var beatViewModel = await this.beatsService.FindBeatByIdAsync<IndexBeatViewModel>(id);
            var usersWhoLikedTheBeat = this.likeService.GetFansLikedTheBeat(id);

            var viewModel = new ByNameViewModel();
            viewModel.Beat = beatViewModel;
            viewModel.Fans = usersWhoLikedTheBeat;

            return this.View(viewModel);
        }
    }
}
