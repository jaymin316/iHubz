using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using iHubz.Domain.Core.Specification;

namespace iHubz.Domain.Core
{
    public interface IRepository<TEntity, TKey> : IDisposable
        where TEntity : DomainEntity
    {
        /// <summary>
        /// Get the unit of work in this repository
        /// </summary>
        [Obsolete("Inject IUnitOfWork to app service - And use DbContext in Repository")]
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        void Add(TEntity item);

        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="item">Item to delete</param>
        void Remove(TEntity item);

        /// <summary>
        /// Set item as modified
        /// </summary>
        /// <param name="item">Item to modify</param>
        void Modify(TEntity item);

        /// <summary>
        ///Track entity into this repository, really in UnitOfWork. 
        ///In EF this can be done with Attach and with Update in NH
        /// </summary>
        /// <param name="item">Item to attach</param>
        void TrackItem(TEntity item);

        /// <summary>
        /// Sets modified entity into the repository. 
        /// When calling Commit() method in UnitOfWork 
        /// these changes will be saved into the storage
        /// </summary>
        /// <param name="persisted">The persisted item</param>
        /// <param name="current">The current item</param>
        void Merge(TEntity persisted, TEntity current);

        /// <summary>
        /// Get element by entity key
        /// </summary>
        /// <param name="id">Entity key value</param>
        /// <returns></returns>
        TEntity Get(TKey id);

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetAll();

       
        /// <summary>
        /// Get all elements of type TEntity that matching a
        /// Specification
        /// </summary>
        /// <param name="specification">Specification that result meet</param>        
        /// <returns></returns>
        IEnumerable<TEntity> AllMatchingBySpec(ISpecification<TEntity> specification);

        /// <summary>
        /// same as all matching except uses the child repository overloaded GetSetWithDependants to allow the spec to 
        /// filter accross joined tables
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="isTracked"></param>
        /// <returns></returns>
        IEnumerable<TEntity> AllMatchingWithDependendants(ISpecification<TEntity> specification, bool isTracked = true);

        /// <summary>
        /// Get all elements of type TChildEntity that matching a
        /// Specification <paramref name="specification"/>
        /// </summary>
        /// <typeparam name="TChildEntity"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        IEnumerable<TChildEntity> AllMatchingWithNoTracking<TChildEntity>(ISpecification<TChildEntity> specification)
            where TChildEntity : DomainEntity;

        /// <summary>
        /// Check whether there is any TEntity that matching a
        /// Specification <paramref name="specification"/>
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <returns></returns>
        bool Exists(ISpecification<TEntity> specification);

        /// <summary>
        /// Get all elements of type TEntity in repository
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression for this query</param>
        /// <param name="ascending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount,
            Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending);

        /// <summary>
        /// Get  elements of type TEntity in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);
    }
}
