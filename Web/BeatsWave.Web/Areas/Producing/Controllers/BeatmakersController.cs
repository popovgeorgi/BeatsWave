namespace BeatsWave.Web.Areas.Producing.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.BeatmakerRoleName)]
    [Area("Producing")]
    public class BeatmakersController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Upload()
        {
            return this.View();
        }
    }
}
