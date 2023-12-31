using Infrastructure.Domain;
using Infrastructure.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {

        
        Task<List<T>> GetAllAsync(IList<Sort> orderBy = null, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<List<T>> GetAllAsync(IList<Sort> orderBy = null, Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties);
        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression, IList<Sort> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression, IList<Sort> orderBy = null, params Expression<Func<T, object>>[] includeProperties);
        Task InsertAsync(T entity);
        Task ChangeState<TEntity>(TEntity entity, EntityState state) where TEntity : class;
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
