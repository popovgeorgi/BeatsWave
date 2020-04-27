namespace BeatsWave.Web.ViewModels.Checkout
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using BeatsWave.Data.Models;
    using BeatsWave.Services.Mapping;

    public class CheckoutBeatViewModel : IMapFrom<Beat>
    {
        public string Name { get; set; }

        public int StandartPrice { get; set; }
    }
}
