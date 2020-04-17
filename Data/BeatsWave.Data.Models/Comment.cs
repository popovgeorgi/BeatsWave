namespace BeatsWave.Data.Models
{
    using BeatsWave.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public int BeatId { get; set; }

        public virtual Beat Beat { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
