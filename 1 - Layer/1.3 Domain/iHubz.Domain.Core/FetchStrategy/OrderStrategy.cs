using System;
using System.Linq;
using System.Linq.Expressions;

namespace iHubz.Domain.Core.FetchStrategy
{
    public class OrderStrategy<TEntity, TOrderValue> : IOrderStrategy<TEntity>
    {
        #region IOrderStrategy<TEntity> Members

        public bool Descending { get; set; }

        private readonly Expression<Func<TEntity, TOrderValue>> _orderValue;

        /// <summary>
        /// Apply new order by to overwrite any existing order by
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query)
        {
            if (query != null)
            {
                return !Descending ? query.OrderBy(_orderValue) : query.OrderByDescending(_orderValue);
            }
            return null;
        }

        /// <summary>
        /// Add order by to existing ones.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IOrderedQueryable<TEntity> ApplyThenBy(IOrderedQueryable<TEntity> query)
        {
            if (query != null)
            {
                return !Descending ? query.ThenBy(_orderValue) : query.ThenByDescending(_orderValue);
            }
            return null;
        }

        #endregion

        /// <summary>
        /// Order strategy
        /// </summary>
        /// <param name="descending"></param>
        /// <param name="orderValue"></param>
        public OrderStrategy(Expression<Func<TEntity, TOrderValue>> orderValue, bool descending)
        {
            Descending = descending;
            _orderValue = orderValue;
        }

        /// <summary>
        /// Order strategy
        /// </summary>        
        /// <param name="orderValue"></param>
        public OrderStrategy(Expression<Func<TEntity, TOrderValue>> orderValue)
            : this(orderValue, false)
        {
        }
    }
}
