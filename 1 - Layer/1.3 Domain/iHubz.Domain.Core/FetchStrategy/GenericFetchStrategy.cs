using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace iHubz.Domain.Core.FetchStrategy
{
    public class GenericFetchStrategy<TEntity> : IFetchStrategy<TEntity>
    {
        private readonly IList<string> _properties;
        public bool IsTracking { get; set; }
        private readonly List<IOrderStrategy<TEntity>> _orderStrategies;

        public IEnumerable<IOrderStrategy<TEntity>> OrderStrategies
        {
            get
            {
                return _orderStrategies;
            }
        }

        public GenericFetchStrategy()
        {
            _properties = new List<string>();
            IsTracking = true;
            _orderStrategies = new List<IOrderStrategy<TEntity>>();
        }

        public GenericFetchStrategy(bool isTracking)
            : this()
        {
            IsTracking = isTracking;
        }

        /// <summary>
        /// Lists of paths to fetch
        /// </summary>
        public virtual IEnumerable<string> IncludePaths
        {
            get { return _properties; }
        }

        /// <summary>
        /// Add path to fetch based on the supplied expression
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IFetchStrategy<TEntity> Include(Expression<Func<TEntity, object>> path)
        {
            _properties.Add(path.ToPropertyName());
            return this;
        }

        /// <summary>
        /// Add path to fetch based on the supplied string
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IFetchStrategy<TEntity> Include(string path)
        {
            _properties.Add(path);
            return this;
        }

        /// <summary>
        /// Add order by
        /// </summary>
        /// <param name="orderValue"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public IFetchStrategy<TEntity> OrderBy<TOrderValueType>(Expression<Func<TEntity, TOrderValueType>> orderValue,
            bool descending = false)
        {
            _orderStrategies.Add(new OrderStrategy<TEntity, TOrderValueType>(orderValue, descending));
            return this;
        }
    }
}
