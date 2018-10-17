using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MRTFramework.CrossCuttingConcern.Utils.Extensions;
using MRTFramework.CrossCuttingConcern.Utils.Interfaces;
using MRTFramework.Model.BaseModels.Abstract;

namespace MRTFramework.CrossCuttingConcern.EntityFrameworkCore
{
    public class EntityFrameworkCoreRepository<TEntity, TContext> : ISyncRepository<TEntity>, IAsyncRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
        where TContext : DbContext
    {
        protected readonly TContext DbContext;

        public EntityFrameworkCoreRepository(TContext dbContext)
        {
            DbContext = dbContext;
        }

        public DbSet<TEntity> DbSet => DbContext.Set<TEntity>();
        public DatabaseFacade Database => DbContext.Database;

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            DbSet.AddRange(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await DbSet.AddRangeAsync(entity);
        }

        public void Update(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void UpdateRange(IEnumerable<TEntity> entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await CommitAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await CommitAsync();
        }

        public void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            await CommitAsync();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? DbSet.FirstOrDefault() : DbSet.FirstOrDefault(predicate);
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await DbSet.FirstOrDefaultAsync();
            }

            return await DbSet.FirstOrDefaultAsync(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? DbSet.SingleOrDefault() : DbSet.SingleOrDefault(predicate);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await DbSet.SingleOrDefaultAsync();
            }

            return await DbSet.SingleOrDefaultAsync(predicate);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Find(predicate);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.FindAsync(predicate);
        }

        public IEnumerable<TEntity> ToAllList(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? DbSet.AsEnumerable() : DbSet.Where(predicate).AsEnumerable();
        }

        public async Task<IEnumerable<TEntity>> ToAllListAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await DbSet.ToListAsync();
            }

            return await DbSet.Where(predicate).ToListAsync();
        }

        public void Commit()
        {
            DbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null ? DbSet.Any() : DbSet.Any(predicate);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return await DbSet.AnyAsync();
            }

            return await DbSet.AnyAsync(predicate);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.CountAsync(predicate);
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, List<string> includes)
        {
            return DbContext.QueryableHelper(predicate, includes).AsEnumerable();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, List<string> includes)
        {
            return await DbContext.QueryableHelper(predicate, includes).ToListAsync();
        }

        public IEnumerable<TEntity> GetListOrderBy(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> sortColumn, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            return result.AsEnumerable().OrderBy(sortColumn);
        }

        public async Task<IEnumerable<TEntity>> GetListOrderByAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> sortColumn, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            return await result.AsEnumerable().OrderBy(sortColumn).ToAsyncEnumerable().ToList();
        }

        public IEnumerable<TEntity> GetListOrderByDesc(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> sortColumn, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            return result.AsEnumerable().OrderByDescending(sortColumn);
        }

        public async Task<IEnumerable<TEntity>> GetListOrderByDescAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> sortColumn, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            return await result.AsEnumerable().OrderByDescending(sortColumn).ToAsyncEnumerable().ToList();
        }

        public IEnumerable<TEntity> GetListByPage(Expression<Func<TEntity, bool>> predicate, ushort pageIndex, ushort pageSize, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            pageIndex = pageIndex < 1 ? ++pageIndex : pageIndex;
            pageSize = pageSize < 1 ? ++pageSize : pageSize;

            return result.Skip((pageIndex - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public async Task<IEnumerable<TEntity>> GetListByPageAsync(Expression<Func<TEntity, bool>> predicate, ushort pageIndex, ushort pageSize, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            pageIndex = pageIndex < 1 ? ++pageIndex : pageIndex;
            pageSize = pageSize < 1 ? ++pageSize : pageSize;

            return await result.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public IEnumerable<TEntity> GetListByPageOrderBy(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> sortColumn, ushort pageIndex, ushort pageSize, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            pageIndex = pageIndex < 1 ? ++pageIndex : pageIndex;
            pageSize = pageSize < 1 ? ++pageSize : pageSize;

            return result.AsEnumerable().OrderBy(sortColumn).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public async Task<IEnumerable<TEntity>> GetListByPageOrderByAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> sortColumn, ushort pageIndex, ushort pageSize, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            pageIndex = pageIndex < 1 ? ++pageIndex : pageIndex;
            pageSize = pageSize < 1 ? ++pageSize : pageSize;

            return await result.AsEnumerable().OrderBy(sortColumn).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToAsyncEnumerable().ToList();
        }

        public IEnumerable<TEntity> GetListByPageOrderByDesc(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> sortColumn, ushort pageIndex, ushort pageSize, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            pageIndex = pageIndex < 1 ? ++pageIndex : pageIndex;
            pageSize = pageSize < 1 ? ++pageSize : pageSize;

            return result.AsEnumerable().OrderByDescending(sortColumn).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public async Task<IEnumerable<TEntity>> GetListByPageOrderByDescAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, object> sortColumn, ushort pageIndex, ushort pageSize, List<string> includes)
        {
            var result = DbContext.QueryableHelper(predicate, includes);

            pageIndex = pageIndex < 1 ? ++pageIndex : pageIndex;
            pageSize = pageSize < 1 ? ++pageSize : pageSize;

            return await result.AsEnumerable().OrderByDescending(sortColumn).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToAsyncEnumerable().ToList();
        }

        public IQueryable<TEntity> GetListQueryable(Expression<Func<TEntity, bool>> predicate, List<string> includes)
        {
            return DbContext.QueryableHelper(predicate, includes);
        }
    }
}
