using System;
using System.Linq.Expressions;
using iHubz.Domain.Core.FetchStrategy;

namespace iHubz.Domain.Core.Specification
{
    public interface ISpecification<TEntity>
       where TEntity : ISpecifiable
    {
        /// <summary>
        /// Check if this specification is satisfied by a 
        /// specific expression lambda
        /// </summary>
        /// <returns></returns>
        Expression<Func<TEntity, bool>> IsSatisfied();

        bool IsSatisfiedBy(TEntity resource);

        /// <summary>
        /// Defines the fetch strategy to eager load related entities
        /// </summary>
        IFetchStrategy<TEntity> FetchStrategy { get; set; }
    }
}
