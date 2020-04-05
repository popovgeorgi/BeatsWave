namespace BeatsWave.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class BeatToBuyViewModel : IMapFrom<Beat>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string ProducerId { get; set; }

        public string Producer { get; set; }

        public string ImageUrl { get; set; }

        public string BeatUrl { get; set; }

        public int StandartPrice { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Beat, BeatToBuyViewModel>()
                .ForMember(a => a.ImageUrl, m => m.MapFrom(a => a.CloudinaryImage.PictureUrl))
                .ForMember(a => a.BeatUrl, m => m.MapFrom(a => a.CloudinaryBeat.BeatUrl))
                .ForMember(a => a.Producer, m => m.MapFrom(a => a.Producer.UserName));
        }
    }
}
