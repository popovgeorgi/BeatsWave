﻿namespace BeatsWave.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BeatsWave.Common;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Data;
    using BeatsWave.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly INotificationsService notificationsService;
        private readonly IBeatsService beatsService;

        public CommentsController(ICommentsService commentsService, UserManager<ApplicationUser> userManager, INotificationsService notificationsService, IBeatsService beatsService)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
            this.notificationsService = notificationsService;
            this.beatsService = beatsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateCommentInputModel inputModel)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.commentsService.Create(inputModel.BeatId, userId, inputModel.Content);

            var targetId = this.beatsService.FindUserIdByBeatId(inputModel.BeatId);
            var beatName = this.beatsService.GetBeatNameByBeatId(inputModel.BeatId);
            await this.notificationsService.SendNotificationAsync(targetId, string.Format(GlobalConstants.CommentNotification, $"{this.User.Identity.Name}", $"{beatName}"), "Comment");

            return this.RedirectToAction("ByName", "Beats", new { id = inputModel.BeatId });
        }
    }
}
