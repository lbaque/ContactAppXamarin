using Infrastructure.Domain;
using Infrastructure.Helpers;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private DbContext _repositoryContext;
        public BaseRepository(DbContext DBcontext)
        {
            _repositoryContext = DBcontext;
            _repositoryContext.Database.EnsureCreated();
        }

        public IQueryable<T> AsQueryable()
        {
            return _repositoryContext.Set<T>().AsQueryable();
        }

        public async Task<List<T>> GetAllAsync(IList<Sort> orderBy = null, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {

            //IQueryable<T> query =  PerformInclusions(include, AsQueryable());
            IQueryable<T> query = include == null ? AsQueryable() : include(AsQueryable());


            if (expression != null)
                query = query.Where(expression);

            if (orderBy != null)
                query = query.OrderBy(orderBy);

         

            int totalRecords = expression == null ?
                AsQueryable().Count() :
                AsQueryable().Where(expression).Count();

            return await Task.FromResult(query.ToList());
        }


        //TODO: Comentar y probar que todo funciona bien, segun parece el metodo de arriba se llevo todas las sobrecargas.
        public async Task<List<T>> GetAllAsync(IList<Sort> orderBy = null, Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = PerformInclusions(includeProperties, AsQueryable());

            if (expression != null)
                query = query.Where(expression);

            if (orderBy != null)
                query = query.OrderBy(orderBy);

            int totalRecords = expression == null ?
                AsQueryable().Count() :
                AsQueryable().Where(expression).Count();

            return await Task.FromResult(query.ToList());
        }

		public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = include == null ? AsQueryable() : include(AsQueryable());
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = PerformInclusions(includeProperties, AsQueryable());
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }


        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression, IList<Sort> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = include == null ? AsQueryable() : include(AsQueryable());
            if (orderBy != null)
                query = query.OrderBy(orderBy);
            return await query.AsNoTracking().LastOrDefaultAsync(expression);
        }

        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> expression, IList<Sort> orderBy = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = PerformInclusions(includeProperties, AsQueryable());
            if (orderBy != null)
                query = query.OrderBy(orderBy);
            return await query.AsNoTracking().LastOrDefaultAsync(expression);
        }



        private IQueryable<T> PerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties, IQueryable<T> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public async Task InsertAsync(T entity)
        {
                await _repositoryContext.Set<T>().AddAsync(entity);

            //TODO: AQUI PODEMOS HACER QUE AGREGUE EN CASCADA LAS PROPIEDADES DEL OBJETO, PRIMERO PROBEMOS DE LA MANERA MAS FACIL
        }

        public async Task ChangeState<TEntity>(TEntity entity, EntityState state) where TEntity : class
        {
            _repositoryContext.Entry<TEntity>(entity).State = state;
            //para eliminar la advertencia
            await Task.CompletedTask;

        }


        public async Task UpdateAsync(T entity)
        {
            _repositoryContext.Set<T>().Update(entity);
            //para eliminar la advertencia
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
            await Task.CompletedTask;
        }

   
        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await _repositoryContext.SaveChangesAsync(true, cancellationToken);
        }


    }
}
