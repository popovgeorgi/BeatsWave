namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Cart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartsService cartsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CartsController(ICartsService cartsService, UserManager<ApplicationUser> userManager)
        {
            this.cartsService = cartsService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CartResponseModel>> Cart(CartInputModel input)
        {
            var userId = this.userManager.GetUserId(this.User);
            var message = await this.cartsService.AddAsync(input.BeatId, userId);
            var count = this.cartsService.Count(userId);
            return new CartResponseModel { Message = message, CartCount = count };
        }
    }
}
