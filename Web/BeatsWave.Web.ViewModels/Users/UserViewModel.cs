namespace BeatsWave.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Beats;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public virtual string UserName { get; set; }

        public virtual ICollection<UserBeatViewModel> Beats { get; set; }
    }
}
