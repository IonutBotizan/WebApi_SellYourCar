using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SellYourCar.DBContext_Related;
using SellYourCar.Entities;

namespace SellYourCar.Repos
{
    public class MakeCarRepo : BaseRepository<MakeCar> , IMakeCarRepo
    {
        public MakeCarRepo(MyContext ctx) : base(ctx)
        {
            
        }   
        public MakeCar GetMakeCarByMoniker(string moniker)
        {
            return _ctx.MakeCarSet.Where(p=> p.Name.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
            .FirstOrDefault();
        }
        public MakeCar GetMakeByMonikerWithModel(string moniker)
        {
            return _ctx
            .MakeCarSet
        .Include(c => c.ModelCars)
        .ThenInclude(s => s.Advertises)
        .Where(c => c.Name.Equals(moniker, StringComparison.CurrentCultureIgnoreCase))
        .FirstOrDefault();
        }
        public MakeCar GetMakeWithModel(int id)
        {
            return _ctx.MakeCarSet.Include(p=>p.ModelCars).ThenInclude(p=>p.Advertises).Where(p=>p.Id==id).FirstOrDefault();
        }
    }
}