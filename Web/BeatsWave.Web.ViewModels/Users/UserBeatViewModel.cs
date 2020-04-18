namespace BeatsWave.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class UserBeatViewModel : IMapFrom<Beat>, IHaveCustomMappings
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public string Producer { get; set; }

        public int Bpm { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Beat, UserBeatViewModel>()
                .ForMember(a => a.ImageUrl, m => m.MapFrom(a => a.CloudinaryImage.PictureUrl));
        }
    }
}
