namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using BeatsWave.Common;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels;
    using BeatsWave.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IBeatsService beatsService;

        public HomeController(IBeatsService beatsService)
        {
            this.beatsService = beatsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> All(int page = 1)
        {
            var viewModel = new IndexViewModel();

            var count = (int)Math.Ceiling((double)await this.beatsService.GetCountAsync() / GlobalConstants.ItemsPerPage);
            viewModel.PagesCount = count;
            viewModel.CurrentPage = page;

            var beats = await this.beatsService.GetAllBeatsAsync(GlobalConstants.ItemsPerPage, (int)(page - 1) * GlobalConstants.ItemsPerPage);
            viewModel.Beats = beats;

            return this.View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
