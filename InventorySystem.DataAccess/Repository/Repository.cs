using InventorySystem.DataAccess.Data;
using InventorySystem.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InventorySystem.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);//add into the table
        }

        public T Get(int id)
        {
            return dbSet.Find(id);//select * from
        }

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string propertyIncluding = null
            )
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (propertyIncluding != null)
            {
                foreach (var propertyToInclude in
                    propertyIncluding.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propertyToInclude);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public T GetFirst(
            Expression<Func<T, bool>> filter = null, 
            string propertyIncluding = null
            )
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            if (propertyIncluding != null)
            {
                foreach (var propertyToInclude in
                    propertyIncluding.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(propertyToInclude);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entity = dbSet.Find(id);
            Remove(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
