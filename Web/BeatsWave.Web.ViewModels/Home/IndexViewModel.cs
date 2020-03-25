namespace BeatsWave.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class IndexViewModel
    {
        public IEnumerable<IndexBeatViewModel> Beats { get; set; }

    }
}
