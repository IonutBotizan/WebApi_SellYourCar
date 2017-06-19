using System;
using System.Collections.Generic;
using SellYourCar.Entities;

namespace SellYourCar.Repos
{
    public interface IMakeCarRepo : IBaseRepository<MakeCar>
    {
         MakeCar GetMakeCarByMoniker(string moniker);
         MakeCar GetMakeByMonikerWithModel(string moniker);

         MakeCar GetMakeWithModel(int id);
    }
    public interface IModelCarRepo : IBaseRepository<ModelCar>
    {
          IEnumerable<ModelCar> GetModelsByMake(string makename);
          ModelCar GetModelByName(string modelname);
    }
    public interface IAdvertiseCarRepo : IBaseRepository<Advertise>
    {
        IEnumerable<Advertise> GetAllAdsFromMake(string makename);
        IEnumerable<Advertise> GetAdvertisesByModelName(string modelname);
        Advertise GetAdvertiseById(int id);
        DateTime GetDateAddedForUser(int id);
    }
}