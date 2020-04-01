namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Home;

    public class BeatsService : IBeatsService
    {
        private readonly IDeletableEntityRepository<Beat> beatRepository;

        public BeatsService(IDeletableEntityRepository<Beat> beatRepository)
        {
            this.beatRepository = beatRepository;
        }

        public T FindBeatById<T>(int id)
        {
            var beat = this.beatRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();

            return beat;
        }

        public IEnumerable<IndexBeatViewModel> GetAllBeats()
        {
            var beats = this.beatRepository.All()
                .To<IndexBeatViewModel>()
                .ToList();

            return beats;
        }
    }
}
