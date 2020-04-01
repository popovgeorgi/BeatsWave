namespace BeatsWave.Web.Areas.Producing.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class BeatsController : Controller
    {
        public IActionResult Edit(int beatId)
        {
            return this.View();
        }
    }
}
