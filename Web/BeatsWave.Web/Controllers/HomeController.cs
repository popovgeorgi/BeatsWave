namespace BeatsWave.Web.Controllers
{
    using System.Diagnostics;
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

        public IActionResult All()
        {
            var viewModel = new IndexViewModel();

            var beats = this.beatsService.GetAllBeats();
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
