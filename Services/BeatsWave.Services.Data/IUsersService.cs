namespace BeatsWave.Services.Data
{
    using BeatsWave.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IUsersService
    {
        T GetById<T>(string id);
    }
}
