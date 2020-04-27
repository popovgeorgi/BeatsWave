namespace BeatsWave.Web.ViewModels.Checkout
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class CheckoutListViewModel : IMapFrom<Cart>
    {
        public int OrderTotal { get; set; }

        public ICollection<CheckoutBeatViewModel> Beats { get; set; }
    }
}
