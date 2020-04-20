namespace BeatsWave.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BeatsWave.Data;
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Data.Repositories;
    using BeatsWave.Web.ViewModels.Likes;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class LikeServiceTests
    {
        [Fact]
        public async Task TwoConsequentLikesShouldCountAsZero()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BeatsWaveVirtual");
            var likeRepository = new EfRepository<Like>(new ApplicationDbContext(options.Options));
            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var service = new LikeService(likeRepository, beatRepository, userRepository);

            await service.VoteAsync(1, "1");
            await service.VoteAsync(1, "1");
            var likes = service.GetLikes(1);

            Assert.Equal(0, likes);
        }

        [Fact]
        public async Task LikesFromThreeDifferentUsersAreCounted()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BeatsWaveVirtual");
            var likeRepository = new EfRepository<Like>(new ApplicationDbContext(options.Options));
            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var service = new LikeService(likeRepository, beatRepository, userRepository);

            await service.VoteAsync(1, "1");
            await service.VoteAsync(1, "2");
            await service.VoteAsync(1, "3");

            var likes = service.GetLikes(1);

            Assert.Equal(3, likes);
        }

        [Fact]
        public async Task CheckIfWhenUserLikesABeatItReturnsTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BeatsWaveVirtual");
            var likeRepository = new EfRepository<Like>(new ApplicationDbContext(options.Options));
            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var service = new LikeService(likeRepository, beatRepository, userRepository);

            await service.VoteAsync(1, "1");
            var result = service.IsLikedByCurrentUser("1", 1);

            Assert.True(result);
        }

        [Fact]
        public void CheckIfWhenUserDidntLikeABeatItReturnsFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BeatsWaveVirtual");
            var likeRepository = new EfRepository<Like>(new ApplicationDbContext(options.Options));
            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var service = new LikeService(likeRepository, beatRepository, userRepository);

            var result = service.IsLikedByCurrentUser("1", 1);

            Assert.False(result);
        }

        [Fact]

        public async Task CheckIfItGetsUsersWhoLikedTheBeat()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase("BeatsWaveVirtual");
            var likeRepository = new EfRepository<Like>(new ApplicationDbContext(options.Options));
            var beatRepository = new EfDeletableEntityRepository<Beat>(new ApplicationDbContext(options.Options));
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var service = new LikeService(likeRepository, beatRepository, userRepository);

            var mockedService = new Mock<ILikeService>();

            mockedService.Setup(p => p.GetFansLikedTheBeat(1)).Returns(new List<FanViewModel>
            {
                new FanViewModel
                {
                    Name = "Pesho",
                    ImageUrl = "prpr",
                },
                new FanViewModel
                {
                    Name = "Gosho",
                    ImageUrl = "prpr",
                },
            });


            await service.VoteAsync(1, "1");
            await service.VoteAsync(1, "2");
            var users = service.GetFansLikedTheBeat(1);
            

        }
    }
}
