namespace BeatsWave.Web.ViewComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Notification;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "Notification")]
    public class NotificationViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationsService notificationsService;

        public NotificationViewComponent(UserManager<ApplicationUser> userManager, INotificationsService notificationsService)
        {
            this.userManager = userManager;
            this.notificationsService = notificationsService;
        }

        public IViewComponentResult Invoke()
        {
            var userId = this.userManager.GetUserId(this.Request.HttpContext.User);
            var viewModel = new NotificationViewModel();
            var count = this.notificationsService.GetUnseenCount(userId);
            viewModel.Count = count;
            viewModel.UserId = userId;

            return this.View(viewModel);
        }
    }
}
