namespace BeatsWave.Web.ViewModels.Beats
{
    using AutoMapper;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class EditBeatViewModel : IMapFrom<Beat>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal StandartPrice { get; set; }

        public string Description { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Beat, EditBeatViewModel>()
                .ForMember(a => a.ImageUrl, m => m.MapFrom(a => a.CloudinaryImage.PictureUrl));
        }
    }
}
