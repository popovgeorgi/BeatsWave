namespace BeatsWave.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "TotalPrice")]
    public class TotalPriceViewComponent : ViewComponent
    {
        private readonly ICartsService cartsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TotalPriceViewComponent(ICartsService cartsService, UserManager<ApplicationUser> userManager)
        {
            this.cartsService = cartsService;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.userManager.GetUserId(this.Request.HttpContext.User);
            var viewModel = new TotalPriceViewModel();
            viewModel.TotalPrice = this.cartsService.TotalPrice(userId);
            return this.View(viewModel);
        }
    }
}
