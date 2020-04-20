using BeatsWave.Data;
using BeatsWave.Data.Models;
using BeatsWave.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BeatsWave.Services.Data.Tests
{
    public class BeatsServiceTests
    {
        [Fact]
        public async Task CheckIfGetCountWorks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BeatsWaveVirtual");
            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<CloudinaryImage>(new ApplicationDbContext(options.Options));
            var service = new BeatsService(beatRepository, imageRepository);

            var result = await service.GetCountAsync();

            Assert.Equal(0, result);
        }
    }
}
