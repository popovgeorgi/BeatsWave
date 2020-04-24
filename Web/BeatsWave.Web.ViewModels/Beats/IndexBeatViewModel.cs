namespace BeatsWave.Web.ViewModels.Beats
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Base;

    public class IndexBeatViewModel : IMapFrom<Beat>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string ProducerId { get; set; }

        public string Producer { get; set; }

        public string ImageUrl { get; set; }
        //mp3
        public string BeatUrl { get; set; }

        public int BeatId { get; set; }

        public int StandartPrice { get; set; }

        public string Description { get; set; }

        public int LikesCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public IEnumerable<BeatCommentViewModel> Comments { get; set; }

        public string Url => $"/b/{this.Name.Replace(' ', '-')}";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Beat, IndexBeatViewModel>()
                .ForMember(a => a.ImageUrl, m => m.MapFrom(a => a.CloudinaryImage.PictureUrl))
                .ForMember(a => a.BeatUrl, m => m.MapFrom(a => a.CloudinaryBeat.BeatUrl))
                .ForMember(a => a.Producer, m => m.MapFrom(a => a.Producer.UserName))
                .ForMember(a => a.BeatId, m => m.MapFrom(a => a.Id))
                .ForMember(a => a.LikesCount, m => m.MapFrom(a => a.Likes.Sum(l => (int)l.Type)));
        }
    }
}