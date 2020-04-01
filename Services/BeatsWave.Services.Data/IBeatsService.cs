namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Web.ViewModels.Home;

    public interface IBeatsService
    {
        IEnumerable<IndexBeatViewModel> GetAllBeats(int? take = null, int skip = 0);

        T FindBeatById<T>(int id);

        int Count();
    }
}
