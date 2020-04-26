namespace BeatsWave.Web.ViewModels.Beats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class BeatSingleCartViewModel : IMapFrom<Beat>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int StandartPrice { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Beat, BeatSingleCartViewModel>()
                .ForMember(a => a.ImageUrl, m => m.MapFrom(a => a.CloudinaryImage.PictureUrl));
        }
    }
}
