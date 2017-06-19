using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SellYourCar.DBContext_Related;
using SellYourCar.Entities;

namespace SellYourCar.Repos
{
    public class ModelCarRepo: BaseRepository<ModelCar> , IModelCarRepo
    {
        public ModelCarRepo(MyContext ctx) :base(ctx)
        {
            
        }

        public ModelCar GetModelByName(string modelname)
        {
            return _ctx.ModelCarSet.Include(p=> p.Make).Where(p=> p.Name.Equals(modelname)).FirstOrDefault();
        }

        public IEnumerable<ModelCar> GetModelsByMake(string makename)
        {
            return _ctx.ModelCarSet.Include(p=> p.Make).Where(p=> p.Make.Name.Equals(makename)).AsEnumerable();
        }
    }
}