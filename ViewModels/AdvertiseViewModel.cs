using System;

namespace SellYourCar.ViewModels
{
    public class AdvertiseViewModel
    {

        public string Url {get; set; }
        public string FullName {get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Mileage { get; set; }
        public int Power { get; set; }

        public int Year { get; set; }
        public string UserWhoAdded { get; set; }
        public byte[] RowVersion { get; set; }

        //lookups
        public string Fuel { get; set; }
        public string[] Fuels {get; set; }

    }
}
