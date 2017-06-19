using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SellYourCar.DBContext_Related;

namespace SellYourCar.Repos
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        protected MyContext _ctx;
        public BaseRepository(MyContext ctx)
        {
            _ctx=ctx;
        }


        public void Add(T entity)
        {
            EntityEntry dbEntityEntry = _ctx.Entry<T>(entity);
            _ctx.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            EntityEntry dbEntityEntry = _ctx.Entry<T>(entity);
             dbEntityEntry.State = EntityState.Deleted;
        }

        public IEnumerable<T> GetAll()
        {
            return _ctx.Set<T>().AsEnumerable();
        }

        public T GetById(int id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public async Task<bool> SaveAllAsync()
        {
           return (await _ctx.SaveChangesAsync()) > 0;

        }

        public void Update(T entity)
        {
            EntityEntry dbEntityEntry = _ctx.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }
        public IEnumerable<T> AllIncluding(params Expression<Func<T,object>>[] associatedEntities)
        {
            IQueryable<T> result = _ctx.Set<T>();
            foreach(var entity in associatedEntities)
            {
                result = result.Include(entity);
            }
            return result.AsEnumerable();
        }

    }
}