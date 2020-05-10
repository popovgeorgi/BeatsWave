namespace BeatsWave.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BeatsWave.Data;
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Data.Repositories;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Beats;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class BeatsServiceTests
    {
        [Fact]
        public async Task CheckIfGetCountWorks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BeatsWaveVirtual");
            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<CloudinaryImage>(new ApplicationDbContext(options.Options));
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var service = new BeatsService(beatRepository, imageRepository, userRepository);

            var result = await service.GetCountAsync();

            Assert.Equal(0, result);
        }

        [Fact]

        public async Task CheckIfGetsBeatByIdAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("BeatsWaveVirtual");

            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<CloudinaryImage>(new ApplicationDbContext(options.Options));
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            await beatRepository.AddAsync(new Beat
            {
                Id = 1,
                Name = "Lit",
                StandartPrice = 20,
                Description = "lalala",
            });
            await beatRepository.SaveChangesAsync();

            var beatService = new BeatsService(beatRepository, imageRepository, userRepository);
            AutoMapperConfig.RegisterMappings(typeof(TestBeat).Assembly);
            var result = await beatService.FindBeatByIdAsync<TestBeat>(1);

            Assert.Equal("Lit", result.Name);
        }

        public class TestBeat : IMapFrom<Beat>
        {
            public string Name { get; set; }
        }

        [Fact]

        public async Task CheckIfReturnsAllBeats()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("BeatsWaveVirtual");

            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<CloudinaryImage>(new ApplicationDbContext(options.Options));
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            for (int i = 0; i < 100; i++)
            {
                await beatRepository.AddAsync(new Beat
                {
                    Name = "Rat",
                });
            }

            await beatRepository.SaveChangesAsync();

            var beatService = new BeatsService(beatRepository, imageRepository, userRepository);
            AutoMapperConfig.RegisterMappings(typeof(TestBeat).Assembly);

            var result = await beatService.GetAllBeatsAsync<TestBeat>(100);

            Assert.Equal(100, result.Count());
        }

        public async Task CheckIfPagingWorks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("BeatsWaveVirtual");

            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var imageRepository = new EfDeletableEntityRepository<CloudinaryImage>(new ApplicationDbContext(options.Options));
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));

            for (int i = 0; i < 100; i++)
            {
                await beatRepository.AddAsync(new Beat
                {
                    Name = "Rat",
                });
            }

            await beatRepository.SaveChangesAsync();

            var beatService = new BeatsService(beatRepository, imageRepository, userRepository);
            AutoMapperConfig.RegisterMappings(typeof(TestBeat).Assembly);

            var result = await beatService.GetAllBeatsAsync<TestBeat>(10);

            Assert.Equal(10, result.Count());
        }

        [Fact]
        public async Task CheckIfUpdateWorks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BeatsWaveVirtual").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Beats.Add(new Beat
            {
                Id = 1,
                Name = "Lit",
                StandartPrice = 20,
                Description = "lalala",
            });
            await dbContext.SaveChangesAsync();

            var beatRepository = new EfDeletableEntityRepository<Beat>(dbContext);
            var imageRepository = new EfDeletableEntityRepository<CloudinaryImage>(dbContext);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new BeatsService(beatRepository, imageRepository, userRepository);

            await service.UpdateAsync(1, "Blq", 20, "lalala");

            Assert.Equal("Blq", dbContext.Beats.FirstOrDefault(x => x.Id == 1).Name);
        }

        [Fact]
        public async Task CheckIfDeleteWorks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BeatsWaveVirtual").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Beats.Add(new Beat
            {
                Id = 1,
                Name = "Lit",
                StandartPrice = 20,
                Description = "lalala",
            });
            await dbContext.SaveChangesAsync();

            var beatRepository = new EfDeletableEntityRepository<Beat>(dbContext);
            var imageRepository = new EfDeletableEntityRepository<CloudinaryImage>(dbContext);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new BeatsService(beatRepository, imageRepository, userRepository);

            await service.Delete(1);

            Assert.Equal(0, dbContext.Beats.Count());
        }
    }
}
