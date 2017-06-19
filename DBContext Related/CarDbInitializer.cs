using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SellYourCar.DBContext_Related
{
    public class CarDbInitializer
    {
        MyContext _ctx;
        private RoleManager<IdentityRole> _roleMgr;
        private UserManager<CarAdderUser> _usrMgr;
        public CarDbInitializer(MyContext ctx, RoleManager<IdentityRole> roleMgr, UserManager<CarAdderUser> usrMgr)
        {
            _ctx = ctx;
            _roleMgr = roleMgr;
            _usrMgr = usrMgr;
        }

        public async Task Seed()
        {
              if(!_ctx.MakeCarSet.Any())
              {
                  _ctx.AddRange(CarsSample);
                  await _ctx.SaveChangesAsync();
              }
              ///User initialize part 
              var user= await _usrMgr.FindByNameAsync("ionutbotizan");
            var user2= await _usrMgr.FindByNameAsync("musatmagdalena");

          if(user==null)
          {
              if(!(await _roleMgr.RoleExistsAsync("Admin")))
              {
                  var role = new IdentityRole("Admin");
                  role.Claims.Add(new IdentityRoleClaim<string>() { ClaimType="IsAdmin", ClaimValue="True" });
                  await _roleMgr.CreateAsync(role);
              }
              user = new CarAdderUser()
              {
                   UserName="ionutbotizan", 
                   FirstName="ionut", 
                   LastName="botizan", 
                   Email ="botizanionutciprian@gmail.com", 
                   PhoneNumber= "0732226130", 
                   Location = "Hunedoara", 
                   SellerType = SellerType.PrivateSeller

              };
              var userResult = await _usrMgr.CreateAsync(user , "P@rola1");
              var roleResult = await _usrMgr.AddToRoleAsync(user , "Admin");
              var claimResult = await _usrMgr.AddClaimAsync(user, new System.Security.Claims.Claim("SuperUser", "True"));
            if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
        {
          throw new InvalidOperationException("Failed to build user and roles");
        }
          }
          if(user2==null)
          {
              if(!(await _roleMgr.RoleExistsAsync("Admin")))
              {
                  var role = new IdentityRole("Admin");
                  role.Claims.Add(new IdentityRoleClaim<string>() { ClaimType="IsAdmin", ClaimValue="True" });
                  await _roleMgr.CreateAsync(role);
              }
              user = new CarAdderUser()
              {
                   UserName="musatmagdalena", 
                   FirstName="magda", 
                   LastName="musat", 
                   Email ="magdalenamusat@gmail.com", 
                   PhoneNumber= "0725226130", 
                   Location="Cluj Napoca", 
                   SellerType = SellerType.PrivateSeller

              };
              var userResult = await _usrMgr.CreateAsync(user , "P@rola2");
              var roleResult = await _usrMgr.AddToRoleAsync(user , "Admin");
              var claimResult = await _usrMgr.AddClaimAsync(user, new System.Security.Claims.Claim("SuperUser", "True"));
            if (!userResult.Succeeded || !roleResult.Succeeded || !claimResult.Succeeded)
        {
          throw new InvalidOperationException("Failed to build user and roles");
        }
          }
        }

        




        List<MakeCar> CarsSample = new List<MakeCar>()
    {
      new MakeCar()
      {
        Name = "Audi",
       
        ModelCars = new List<ModelCar>
        {
          new ModelCar()
          {
            Name = "A 4",
            Advertises= new List<Advertise>()
            {
              new Advertise()
              {
                 Description="Best Car in the world",
                 Price=6500, 
                 Mileage=25000, 
                 DateAdded = DateTime.Now.AddDays(-36).AddHours(-5), 
                 DateUpdated = DateTime.Now.AddDays(-30).AddHours(-3),
                 
                 PhotoUrl ="Audi_A4_1.jpg", 
                 Year =2011 , 
                 Power = 145,
                 Fuel = Fuel.Diesel, 
                 UserWhoAdded="Ionut"
              },
              new Advertise()
              {
                Description="Efficient in cosume",
                Price=9500, 
                Mileage=185500, 
                 DateAdded = DateTime.Now.AddDays(-20).AddHours(-5), 
                 PhotoUrl ="Audi_A4_2.jpg", 
                 Year=2010, 
                 Power = 215,
                 Fuel = Fuel.Diesel, 
                 UserWhoAdded="Ionut"
              },
            }
          },
          new ModelCar()
          {
            Name = "Q 7",
           
            Advertises = new List<Advertise>()
            {
              new Advertise()
              {
                Description="Best SUV on the market", 
                Price=23400, 
                Mileage=163000, 
                 DateAdded = DateTime.Now.AddDays(-22), 
                 PhotoUrl ="Audi_Q7_1.jpg", 
                 Year=2013, 
                 Power = 235,
                 Fuel = Fuel.Petrol, 
                 UserWhoAdded="RAC"
              }
            }
          }
        }
      }
    };
        
    }
}
