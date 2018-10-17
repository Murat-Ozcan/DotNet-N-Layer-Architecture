using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MRTFramework.Model.BaseModels.Abstract;

namespace MRTFramework.CrossCuttingConcern.Utils.Extensions
{
    public static class EntityFrameworkCoreExtensions
    {
        public static IQueryable<TEntity> QueryableHelper<TEntity>
            (this DbContext context, Expression<Func<TEntity, bool>> predicate, IReadOnlyCollection<string> includes = null)
            where TEntity : class, IBaseEntity, new()
        {
            var queryable = predicate != null ? context.Set<TEntity>().Where(predicate) : context.Set<TEntity>();

            if (includes == null || !includes.Any()) return queryable;

            queryable = includes.Aggregate(queryable, (current, include) => current.Include(include));

            return queryable;
        }
    }
}
