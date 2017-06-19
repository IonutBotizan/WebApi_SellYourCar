using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SellYourCar.Repos
{
    public interface IBaseRepository<T> where T: class 
    {
    IEnumerable<T> GetAll();
    IEnumerable<T> AllIncluding(params Expression<Func<T,object>>[] associatedEntities);
    T GetById(int id);        
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);

    Task<bool> SaveAllAsync();

    }
}