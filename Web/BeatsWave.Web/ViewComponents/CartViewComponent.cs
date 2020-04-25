namespace BeatsWave.Web.ViewComponents
{

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "Cart")]
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsService cartsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartViewComponent(ICartsService cartsService, UserManager<ApplicationUser> userManager)
        {
            this.cartsService = cartsService;
            this.userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.userManager.GetUserId(this.Request.HttpContext.User);
            var viewModel = new CartViewModel();
            viewModel.CartCount = this.cartsService.Count(userId);

            return this.View(viewModel);
        }
    }
}
