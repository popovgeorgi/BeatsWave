namespace BeatsWave.Web.ViewModels.Beats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class BeatCartViewModel : IMapFrom<Cart>
    {
        public virtual IEnumerable<BeatSingleCartViewModel> Beats { get; set; }
    }
}
