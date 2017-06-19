using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SellYourCar.DBContext_Related;
using SellYourCar.Entities;

namespace SellYourCar.Repos
{
    public class AdvertiseCarRepo : BaseRepository<Advertise>, IAdvertiseCarRepo
    {
        public AdvertiseCarRepo(MyContext ctx): base(ctx)
        {
            
        }
        public IEnumerable<Advertise> GetAllAdsFromMake(string makename)
        {
            return _ctx.AdvertiseSet.Include(p=> p.ModelCar).ThenInclude(p=> p.Make)
               .Where(p=> p.ModelCar.Make.Name.Equals(makename)).AsEnumerable();
        }
        public IEnumerable<Advertise> GetAdvertisesByModelName(string modelname)
        {
            return _ctx.AdvertiseSet.Include(p=> p.ModelCar).ThenInclude(p=> p.Make)
            .Where(p=> p.ModelCar.Name.Equals(modelname)).AsEnumerable();
        }
        public Advertise GetAdvertiseById(int id)
        {
            return _ctx.AdvertiseSet.Include(p=> p.ModelCar).ThenInclude(p=> p.Make)
               .Where(a=> a.Id==id).FirstOrDefault();
        }
        public DateTime GetDateAddedForUser(int id)
        {
             var add = GetAdvertiseById(id);
             return add.DateAdded;
        }
    }
}