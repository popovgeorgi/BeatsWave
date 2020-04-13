namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Http;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IPictureService pictureService;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository, IPictureService pictureService)
        {
            this.usersRepository = usersRepository;
            this.pictureService = pictureService;
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

            await this.pictureService.UploadImageAsync(id, picture);
        }
    }
}
