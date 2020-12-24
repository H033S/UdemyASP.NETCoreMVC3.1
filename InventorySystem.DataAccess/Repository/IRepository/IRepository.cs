using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace InventorySystem.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T :class
    {
        T Get(int id);
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string propertyIncluding = null);
        T GetFirst(
            Expression<Func<T, bool>> filter = null,
            string propertyIncluding = null);
        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
