using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MRTFramework.Model.BaseModels.Abstract;

namespace MRTFramework.CrossCuttingConcern.Utils.Interfaces
{
    public interface IAsyncRepository<T> where T : class, IBaseEntity, new()
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entity);
        Task DeleteAsync(T entity);
        Task CommitAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate = null);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> ToAllListAsync(Expression<Func<T, bool>> predicate = null);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate, List<string> includes);
        Task<IEnumerable<T>> GetListOrderByAsync(Expression<Func<T, bool>> predicate, Func<T, object> sortColumn, List<string> includes);
        Task<IEnumerable<T>> GetListOrderByDescAsync(Expression<Func<T, bool>> predicate, Func<T, object> sortColumn, List<string> includes);
        Task<IEnumerable<T>> GetListByPageAsync(Expression<Func<T, bool>> predicate, ushort pageIndex, ushort pageSize, List<string> includes);
        Task<IEnumerable<T>> GetListByPageOrderByAsync(Expression<Func<T, bool>> predicate, Func<T, object> sortColumn, ushort pageIndex, ushort pageSize, List<string> includes);
        Task<IEnumerable<T>> GetListByPageOrderByDescAsync(Expression<Func<T, bool>> predicate, Func<T, object> sortColumn, ushort pageIndex, ushort pageSize, List<string> includes);
    }
}
