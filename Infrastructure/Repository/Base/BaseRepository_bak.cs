using Infrastructure.Domain;
using Infrastructure.Helpers;
using Infrastructure.Models.Interfaces;
using Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure.Repository.Base
{
    public class BaseRepository_bak<T> : IBaseRepository<T> where T : class
    {
        private DbContext _repositoryContext;

        public BaseRepository_bak(DbContext DBcontext/*, IHttpContextAccessor httpContextAccessor, bool allowAnonymous = false*/)
        {
            _repositoryContext = DBcontext;
        }
                
        /*SPPROCEDIMIENTO PAR OBTENER TODOS LOS DATOS de SQL */
        public List<T> FindByStoreProcedure(int? skip, int? take, string sql, params object[] includeProperties)
        {

            using (var dbcontext = new ContextForQueryType<T>(_repositoryContext.Database.GetDbConnection()))
            {
                return dbcontext.Set<T>().FromSqlRaw(sql, includeProperties).AsNoTracking().ToList();
            }
        }

        private class ContextForQueryType<T> : DbContext where T : class
        {
            private readonly System.Data.Common.DbConnection connection;

            public ContextForQueryType(System.Data.Common.DbConnection connection)
            {
                this.connection = connection;
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(connection, options => options.EnableRetryOnFailure());

                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<T>().HasNoKey();
                base.OnModelCreating(modelBuilder);
            }
        }

        public IQueryable<T> AsQueryable()
        {
            return _repositoryContext.Set<T>().AsQueryable();
        }

        public PagedResponse<List<T>> GetAll(PaginationFilter paginationFilter, IList<Sort> orderBy = null, Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = PerformInclusions(includeProperties, AsQueryable());

            if (expression != null)
                query = query.Where(expression);

            if (orderBy != null)
                query = query.OrderBy(orderBy);

            query = query
                .Skip((paginationFilter.NumeroPagina - 1) * paginationFilter.TamanioPagina)
                .Take(paginationFilter.TamanioPagina);

            int totalRecords = expression == null ?
                AsQueryable().Count() :
                AsQueryable().Where(expression).Count();

            var result = new PagedResponse<List<T>>(query.ToList(), paginationFilter.NumeroPagina, paginationFilter.TamanioPagina);
            result.TotalRegistros = totalRecords;
            return result;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = PerformInclusions(includeProperties, AsQueryable());
            return query.AsNoTracking().FirstOrDefault(expression);
        }
        private IQueryable<T> PerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties, IQueryable<T> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public void Insert(T entity)
        {
            ModificationAudit(ref entity);
            _repositoryContext.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            NewAudit(ref entity);
            _repositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {

            if (entity is ISoftDeleted)
            {
                ((ISoftDeleted)entity).Estado = "E";
                ((ISoftDeleted)entity).Deleted = true;
                ((ISoftDeleted)entity).DeletedById = Guid.NewGuid();//"quemado x ahora";// _user.Id;
                ((ISoftDeleted)entity).DeletedAt = DateTime.Now;
                _repositoryContext.Set<T>().Attach(entity);
            }
            _repositoryContext.Set<T>().Remove(entity);

        }
        private void NewAudit(ref T entry)
        {
            if (entry is IAuditEntry)
            {
                ((IAuditEntry)entry).CreatedById = Guid.NewGuid();//aqui va quemado x ahora _user.Id;
                ((IAuditEntry)entry).CreatedAt = DateTime.Now;
            }
        }
        private void ModificationAudit(ref T entry)
        {
            if (entry is IAuditEntry)
            {
                ((IAuditEntry)entry).UpdatedById = null;
                ((IAuditEntry)entry).UpdatedAt = DateTime.Now;
            }
        }
        public int SaveChanges()
        {
            return _repositoryContext.SaveChanges();
        }
        public bool Any(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            return PerformInclusions(includeProperties, AsQueryable()).Any(expression);
        }

        
        //-----------------------ASYNC-----------------
        public Task<List<T>> GetAllAsync(int? skip, int? take, Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query;
            if (skip != null && take != null)
                query = PerformInclusions(includeProperties, AsQueryable().Skip(skip.Value).Take(take.Value));
            else
                query = PerformInclusions(includeProperties, AsQueryable());

            return expression == null ? query.AsNoTracking().ToListAsync() : query.Where(expression).AsNoTracking().ToListAsync();
        }
        public Task<T> FirstAsync(Expression<Func<T, bool>> expression = null, params Expression<Func<T, object>>[] includeProperties)
        {
            return PerformInclusions(includeProperties, AsQueryable()).FirstAsync(expression);
        }
        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            return PerformInclusions(includeProperties, AsQueryable()).FirstOrDefaultAsync(expression);
        }
        public Task<int> SaveChangesAsync()
        {
            return _repositoryContext.SaveChangesAsync();
        }
        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includeProperties)
        {
            return PerformInclusions(includeProperties, AsQueryable()).AnyAsync(expression);
        }
        public Task<List<T>> FindByStoreProcedureAsync(int? skip, int? take, string sql, params object[] includeProperties)
        {
            using (var dbcontext = new ContextForQueryType<T>(_repositoryContext.Database.GetDbConnection()))
            {
                return dbcontext.Set<T>().FromSqlRaw(sql, includeProperties).AsNoTracking().ToListAsync();
            }
        }


    }
}
