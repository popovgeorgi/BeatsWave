using BeatsWave.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatsWave.Web.Areas.Producing.Controllers
{
    [Authorize(Roles = GlobalConstants.BeatmakerRoleName)]
    [Area("Producing")]
    public class BeatmakersController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
