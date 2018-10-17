using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MRTFramework.Model.BaseModels.Abstract;

namespace MRTFramework.CrossCuttingConcern.Utils.Interfaces
{
    public interface ISyncRepository<T> where T : class, IBaseEntity, new()
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entity);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entity);
        void Delete(T entity);
        void Commit();
        bool Any(Expression<Func<T, bool>> predicate = null);
        int Count(Expression<Func<T, bool>> predicate = null);
        T FirstOrDefault(Expression<Func<T, bool>> predicate = null);
        T SingleOrDefault(Expression<Func<T, bool>> predicate = null);
        T Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> ToAllList(Expression<Func<T, bool>> predicate = null);
        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate, List<string> includes);
        IEnumerable<T> GetListOrderBy(Expression<Func<T, bool>> predicate, Func<T, object> sortColumn, List<string> includes);
        IEnumerable<T> GetListOrderByDesc(Expression<Func<T, bool>> predicate, Func<T, object> sortColumn, List<string> includes);
        IEnumerable<T> GetListByPage(Expression<Func<T, bool>> predicate, ushort pageIndex, ushort pageSize, List<string> includes);
        IEnumerable<T> GetListByPageOrderBy(Expression<Func<T, bool>> predicate, Func<T, object> sortColumn, ushort pageIndex, ushort pageSize, List<string> includes);
        IEnumerable<T> GetListByPageOrderByDesc(Expression<Func<T, bool>> predicate, Func<T, object> sortColumn, ushort pageIndex, ushort pageSize, List<string> includes);
        IQueryable<T> GetListQueryable(Expression<Func<T, bool>> predicate, List<string> includes);
    }
}
