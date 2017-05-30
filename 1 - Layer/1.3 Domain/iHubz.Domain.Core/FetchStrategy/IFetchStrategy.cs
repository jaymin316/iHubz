using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace iHubz.Domain.Core.FetchStrategy
{
    public interface IFetchStrategy<TEntity>
    {
        IEnumerable<string> IncludePaths { get; }
        IEnumerable<IOrderStrategy<TEntity>> OrderStrategies { get; }

        IFetchStrategy<TEntity> Include(Expression<Func<TEntity, object>> path);

        IFetchStrategy<TEntity> Include(string path);

        bool IsTracking { get; set; }

        /// <summary>
        /// Add order by
        /// </summary>
        /// <param name="orderValue"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        IFetchStrategy<TEntity> OrderBy<TOrderValueType>(Expression<Func<TEntity, TOrderValueType>> orderValue,
            bool descending = false);
    }
}
