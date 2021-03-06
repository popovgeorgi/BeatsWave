﻿namespace BeatsWave.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class UsersLikedBeatsViewModel : IMapFrom<ApplicationUser>
    {
        public virtual ICollection<UserLikesViewModel> Likes { get; set; }
    }
}
