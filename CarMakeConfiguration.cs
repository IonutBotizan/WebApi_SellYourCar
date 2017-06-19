using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SellYourCar.Entities;

namespace SellYourCar.DBContext_Related
{
    //to do refactor into a EntityBaseConfiguration since all these 3 generate the RowVersion
    public class CarMakeConfiguration
    {
        public CarMakeConfiguration(EntityTypeBuilder<MakeCar> builder)
        {
            builder.Property(c => c.RowVersion)
        .ValueGeneratedOnAddOrUpdate()
        .IsConcurrencyToken();
            builder.Property(c => c.Name)
            .HasMaxLength(25);
            builder.HasKey(p => p.Id);
        }
    }
    public class CarModelConfiguration
    {
        public CarModelConfiguration(EntityTypeBuilder<ModelCar> builder)
        {
            builder.Property(c => c.RowVersion)
       .ValueGeneratedOnAddOrUpdate()
       .IsConcurrencyToken();
            builder.Property(p => p.Name).HasMaxLength(25);
            builder.HasKey(p => p.Id);
        }
    }
    public class AdvertiseConfiguration
    {
        public AdvertiseConfiguration(EntityTypeBuilder<Advertise> builder)
        {
            builder.Property(c => c.RowVersion)
        .ValueGeneratedOnAddOrUpdate()
        .IsConcurrencyToken();

            builder.HasKey(p => p.Id);
            //builder.HasOne(p => p.ModelCar).WithMany(p => p.Advertises).HasForeignKey(p => p.ModelCar);
            builder.Property(p => p.Description).HasMaxLength(300);
            builder.Property(p => p.Fuel).HasDefaultValue(Fuel.Diesel);
            builder.Property(p => p.DateUpdated).HasDefaultValue(DateTime.Now);
            builder.Property(p => p.UserWhoAdded).IsRequired();
            builder.Property(p => p.Mileage).IsRequired();
            builder.Property(p => p.Year).IsRequired();
            builder.Property(p => p.Price).IsRequired();

        }
    }
}