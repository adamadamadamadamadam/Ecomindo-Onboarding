using DAL.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet;

        public DbContext Context { get; private set; }
        public Repository(DbContext dbContext)
        {
            Context = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public T Add(T entity)
        {
            var result = dbSet.Add(entity);

            return result.Entity;
        }

        public async Task<T> AddAsync(T entity)
        {
            var result = await dbSet.AddAsync(entity);

            return result.Entity;
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
            {
                dbSet.Remove(obj);
            }
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
}
