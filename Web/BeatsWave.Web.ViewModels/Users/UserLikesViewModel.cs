namespace BeatsWave.Web.ViewModels.Users
{
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class UserLikesViewModel : IMapFrom<Like>
    {
        public int BeatId { get; set; }

        public int Type { get; set; }
    }
}
