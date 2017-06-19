using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SellYourCar.Entities
{
    public class CarAdderUser : IdentityUser
    {
        public string FirstName {get;set;}
        public string LastName {get; set; }

        public string Location {get; set; }
        public SellerType SellerType{get; set; }
        
    }
    public enum SellerType 
    {
        PrivateSeller = 1 , 
        Dealer=2
    }
}