using System.Linq;

namespace iHubz.Domain.Core.FetchStrategy
{
    public interface IOrderStrategy<TEntity>
    {
        bool Descending { get; }

        /// <summary>
        /// Apply new order by to overwrite any existing order by
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query);

        /// <summary>
        /// Add order by to existing ones.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IOrderedQueryable<TEntity> ApplyThenBy(IOrderedQueryable<TEntity> query);
    }
}
