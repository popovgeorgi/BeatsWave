namespace BeatsWave.Web.ViewModels.Beats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Web.ViewModels.Likes;

    public class ByNameViewModel
    {
        public IndexBeatViewModel Beat { get; set; }

        public IEnumerable<FanViewModel> Fans { get; set; }
    }
}
