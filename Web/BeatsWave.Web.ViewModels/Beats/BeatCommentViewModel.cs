namespace BeatsWave.Web.ViewModels.Beats
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class BeatCommentViewModel : IMapFrom<Comment>
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string UserUserName { get; set; }
    }
}
