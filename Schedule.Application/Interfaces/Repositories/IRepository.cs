using Schedule.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Schedule.Application.Interfaces.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : BaseEntity
    {
        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entity);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, string includeProperties);

        Task<TEntity> GetAsync(object id);

        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        void Remove(TEntity entity);

        void RemoveRange(IEnumerable<TEntity> entity);

        void Update(TEntity entity);
    }
}
