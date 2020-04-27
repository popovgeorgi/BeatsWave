namespace BeatsWave.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using BeatsWave.Common;
    using BeatsWave.Data.Common.Repositories;
    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;
    using BeatsWave.Web.ViewModels.Beats;
    using BeatsWave.Web.ViewModels.Checkout;

    public class CartsService : ICartsService
    {
        private readonly IRepository<Cart> cartRepository;
        private readonly IDeletableEntityRepository<Beat> beatsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public CartsService(IRepository<Cart> cartRepository, IDeletableEntityRepository<Beat> beatsRepository, IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.cartRepository = cartRepository;
            this.beatsRepository = beatsRepository;
            this.userRepository = userRepository;
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

        public T GetCartBeats<T>(string userId)
        {
            var beats = this.cartRepository
                .All()
                .Where(x => x.UserId == userId)
                .To<T>()
                .FirstOrDefault();

            return beats;
        }

        public ICollection<CheckoutBeatViewModel> GetCartBeatsSecond(string userId)
        {
            var beats = this.cartRepository
                 .All()
                 .Where(x => x.UserId == userId)
                 .Select(x => x.Beats)
                 .FirstOrDefault();
;

            var output = beats
                .Select(x => new CheckoutBeatViewModel
                {
                    Name = x.Name,
                    StandartPrice = x.StandartPrice,
                })
                .ToList();

            return output;
        }

        public bool Remove(int beatId, string userId)
        {
            var beat = this.beatsRepository
                .All()
                .Where(x => x.Id == beatId)
                .FirstOrDefault();

            var isRemoved = this.cartRepository
                .All()
                .FirstOrDefault(x => x.UserId == userId)
                .Beats
                .Remove(beat);

            return isRemoved;
        }

        public int TotalPrice(string userId)
        {
            var price = this.cartRepository
                .All()
                .Where(x => x.UserId == userId)
                .Select(x => x.Beats.Sum(b => b.StandartPrice))
                .FirstOrDefault();

            return price;
        }
    }
}
