namespace BeatsWave.Services.Data.Home
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

        public IEnumerable<IndexBeatViewModel> GetAllBeats()
        {
            var beats = this.beatRepository.All()
                .To<IndexBeatViewModel>()
                .ToList();

            return beats;
        }
    }
}
