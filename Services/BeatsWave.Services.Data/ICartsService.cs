using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BeatsWave.Services.Data
{
    public interface ICartsService
    {
        Task<string> AddAsync(int beatId, string userId);

        Task CreateCart(string userId);

        int Count(string userId);
    }
}
