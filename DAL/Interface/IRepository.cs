using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        IQueryable<T> GetAll();
        T Add(T entity);
        Task<T> AddAsync(T entity);
        void Delete(Expression<Func<T, bool>> where);
        void Update(T entity);
    }
}
