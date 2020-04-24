namespace BeatsWave.Services.Data.Tests
{
    using BeatsWave.Data;
    using BeatsWave.Data.Models;
    using BeatsWave.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;

    public class CommentsServiceTests
    {
        [Fact]
        public async Task CheckIfCreateWorks()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BeatsWaveVirtual").Options;
            var dbContext = new ApplicationDbContext(options);
            var commentsRepository = new EfDeletableEntityRepository<Comment>(dbContext);

            var service = new CommentsService(commentsRepository);
            await service.Create(1, "asdasd", "lit");

            var result = commentsRepository.All()
                .FirstOrDefault(x => x.Id == 1);

            Assert.Equal("lit", result.Content);
        }
    }
}
