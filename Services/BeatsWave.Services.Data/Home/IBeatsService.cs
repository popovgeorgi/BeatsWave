namespace BeatsWave.Services.Data.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Web.ViewModels.Home;

    public interface IBeatsService
    {
        IEnumerable<IndexBeatViewModel> GetAllBeats();
    }
}
