using System;
using System.ComponentModel.DataAnnotations;

namespace SellYourCar.Entities
{
    //To do ... Add a entity fror photos some more photos than just 1 
    // to Do ... Add some AditionalFeatures Entity to match here automatic gearbox , parking sensors ...etc
    //to do ....maybe useful an AdvertiseBrief , but we can control that from AutoMapper
    public class Advertise
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [Range(1, 2000000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal Price { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public string PhotoUrl { get; set; }
        [Range(0, 3000000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public decimal Mileage { get; set; }
        [Range(0, 2000, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int Power { get; set; }

        [Range(1950, 2017, ErrorMessage = "Value for {0} must be between {1} and {2}")]
        public int Year { get; set; }
        //UserWhoAdded should be not string but User from the Identity generated table 
        [Required]
        public string UserWhoAdded { get; set; }
        //needed here to although only 1 user can change 1 add maybe the admin too 
        public byte[] RowVersion { get; set; }

        public Fuel Fuel { get; set; }

        //the navigation pproperty
        public virtual ModelCar ModelCar { get; set; }
    }

    public enum Fuel
    {
        Diesel = 1,
        Petrol = 2,
        AutoGaz = 3,
        Electric = 4
    }
}