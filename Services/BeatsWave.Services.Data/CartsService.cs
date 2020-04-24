namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BeatsWave.Common;
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;

    public class CartsService : ICartsService
    {
        private readonly IRepository<Cart> cartRepository;
        private readonly IDeletableEntityRepository<Beat> beatsRepository;

        public CartsService(IRepository<Cart> cartRepository, IDeletableEntityRepository<Beat> beatsRepository)
        {
            this.cartRepository = cartRepository;
            this.beatsRepository = beatsRepository;
        }

        public async Task<string> AddAsync(int beatId, string userId)
        {
            var beat = this.beatsRepository
                .All()
                .FirstOrDefault(x => x.Id == beatId);

            var cart = this.cartRepository
                    .All()
                    .FirstOrDefault(x => x.UserId == userId);

            string result = null;

            if (cart.Beats.Contains(beat) == false)
            {
                cart.Beats.Add(beat);
                await this.cartRepository.SaveChangesAsync();
                result = GlobalConstants.CartSuccess;
            }
            else
            {
                result = GlobalConstants.CartContain;
            }

            return result;
        }

        public int Count(string userId)
        {
            return this.cartRepository
                .All()
                .Where(x => x.UserId == userId)
                .Select(x => x.Beats.Count)
                .FirstOrDefault();
        }

        public async Task CreateCart(string userId)
        {
            var cart = new Cart
            {
                UserId = userId,
            };

            await this.cartRepository.AddAsync(cart);
            await this.cartRepository.SaveChangesAsync();
        }
    }
}
