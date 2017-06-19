using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SellYourCar.Entities;

namespace SellYourCar.DBContext_Related
{
    public class MyContext : IdentityDbContext<CarAdderUser>
    {
        //using _cfgRoot just to point to the connectionstring
        //in a non .netcore  we would have : base("connectionString")
        IConfigurationRoot _cfgRoot;
        public MyContext(DbContextOptions options ,IConfigurationRoot config )
        :base(options)
        {
           _cfgRoot = config;    
        }

        //DbSets
        public DbSet<MakeCar> MakeCarSet {get; set;}
        public DbSet<ModelCar> ModelCarSet {get; set; }
        public DbSet<Advertise> AdvertiseSet {get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
base.OnModelCreating(builder);

            builder.Entity<MakeCar>().ToTable("Make");
            builder.Entity<ModelCar>().ToTable("Model");
            builder.Entity<Advertise>().ToTable("Advertise");
           
           new CarMakeConfiguration(builder.Entity<MakeCar>());
           new CarModelConfiguration(builder.Entity<ModelCar>());
           new AdvertiseConfiguration(builder.Entity<Advertise>());

  
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);

      optionsBuilder.UseSqlServer(_cfgRoot["Data:ConnectionString"]);
    }
        
    }
}