namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Models;
    using BeatsWave.Web.ViewModels.Users;
    using Microsoft.AspNetCore.Http;

    public interface IUsersService
    {
        T GetById<T>(string id);

        void UploadProfilePictureAsync(string id, IFormFile picture);
    }
}
