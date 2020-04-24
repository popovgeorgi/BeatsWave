namespace BeatsWave.Services.Data
{
    using System.Linq;

    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Http;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IPictureService pictureService;
        private readonly IDeletableEntityRepository<Beat> beatsRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository, IPictureService pictureService, IDeletableEntityRepository<Beat> beatsRepository)
        {
            this.usersRepository = usersRepository;
            this.pictureService = pictureService;
            this.beatsRepository = beatsRepository;
        }

        public T GetById<T>(string id)
        {
            var user = this.usersRepository
                 .All()
                 .Where(x => x.Id == id)
                 .To<T>()
                 .FirstOrDefault();

            return user;
        }

        public async void UploadProfilePictureAsync(string id, IFormFile picture)
        {
            var user = this.usersRepository
                .All()
                .Where(x => x.Id == id)
                .To<UserProfilePictureViewModel>()
                .FirstOrDefault();

            if (user.ProfilePictureId != 0)
            {
                var pictureId = user.ProfilePictureId;

                await this.pictureService.DeleteImageAsync(pictureId);
            }

            var profilePicId = await this.pictureService.UploadImageAsync(id, picture);

            user.ProfilePictureId = profilePicId;
            await this.usersRepository.SaveChangesAsync();
        }
    }
}
