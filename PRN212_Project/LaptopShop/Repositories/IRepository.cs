using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LaptopShop.Repositories
{
    // Repository Pattern: Abstract data access operations
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);
        int SaveChanges();
    }
}
