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
    using BeatsWave.Web.ViewModels.Checkout;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class CheckoutController : Controller
    {
        private readonly ICartsService cartsService;

        public CheckoutController(ICartsService cartsService)
        {
            this.cartsService = cartsService;
        }

        [Authorize]

        public IActionResult Products()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = this.cartsService.GetCartBeats<BeatCartViewModel>(userId);
            if (viewModel == null)
            {
                return this.RedirectToAction("Feed", "Home");
            }

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Payment()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var viewModel = new CheckoutListViewModel();
            viewModel.OrderTotal = this.cartsService.TotalPrice(userId);
            viewModel.Beats = this.cartsService.GetCartBeatsSecond(userId);

            return this.View(viewModel);
        }
    }
}
