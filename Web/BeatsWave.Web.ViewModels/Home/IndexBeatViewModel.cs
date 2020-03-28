namespace BeatsWave.Web.ViewModels.Home
{
    using AutoMapper;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class IndexBeatViewModel : IMapFrom<Beat>, IHaveCustomMappings
    {
        public string Name { get; set; }

        public string Producer { get; set; }

        public string ImageUrl { get; set; }
        //mp3
        public string BeatUrl { get; set; }

        public decimal StandartPrice { get; set; }

        public string Description { get; set; }

        public string Url => $"/b/{this.Name.Replace(' ', '-')}";

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Beat, IndexBeatViewModel>()
                .ForMember(a => a.ImageUrl, m => m.MapFrom(a => a.CloudinaryImage.PictureUrl))
                .ForMember(a => a.BeatUrl, m => m.MapFrom(a => a.CloudinaryBeat.BeatUrl))
                .ForMember(a => a.Producer, m => m.MapFrom(a => a.Producer.UserName));
        }
    }
}