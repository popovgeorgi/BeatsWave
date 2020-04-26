namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Beats;
    using BeatsWave.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Mvc;

    public class CheckoutController : Controller
    {
        private readonly ICartsService cartsService;

        public CheckoutController(ICartsService cartsService)
        {
            this.cartsService = cartsService;
        }

        public IActionResult Products()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = this.cartsService.GetCartBeats<BeatCartViewModel>(userId);
            return this.View(viewModel);
        }
    }
}
