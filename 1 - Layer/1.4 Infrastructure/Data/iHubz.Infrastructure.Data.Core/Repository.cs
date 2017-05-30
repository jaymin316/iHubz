using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using iHubz.Domain.Core;
using iHubz.Domain.Core.FetchStrategy;
using iHubz.Domain.Core.Specification;

namespace iHubz.Infrastructure.Data.Core
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
      where TEntity : DomainEntity
    {
        #region Members

        protected IQueryableUnitOfWork DbContext { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            DbContext = unitOfWork;
        }

        #endregion

        #region IRepository Members

        [Obsolete("Inject IUnitOfWork to app service - And use DbContext in Repository")]
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return DbContext;
            }
        }

        public virtual void Add(TEntity item)
        {

            if (item != null)
                GetSet().Add(item); // add new item in this set
            //else
            //{
            //    LoggerFactory.CreateLog()
            //              .LogInfo(Messages.info_CannotAddNullEntity, typeof(TEntity).ToString());

            //}

        }

        public virtual void Remove(TEntity item)
        {
            if (item != null)
            {
                //attach item if not exist
                DbContext.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            //else
            //{
            //    LoggerFactory.CreateLog()
            //              .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            //}
        }

        public virtual void TrackItem(TEntity item)
        {
            if (item != null)
                DbContext.Attach(item);
            //else
            //{
            //    LoggerFactory.CreateLog()
            //              .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            //}
        }

        public virtual void Modify(TEntity item)
        {
            if (item != null)
                DbContext.SetModified(item);
            //else
            //{
            //    LoggerFactory.CreateLog()
            //              .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TEntity).ToString());
            //}
        }

        public virtual TEntity Get(TKey id)
        {
            if (!IsEmptyKey(id))
                return GetSet().Find(id);
            else
                return null;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetSet();
        }

        /// <summary>
        /// Get all elements of type TEntity that matching a
        /// Specification
        /// </summary>
        /// <param name="specification">Specification that result meet</param>        
        /// <returns></returns>
        public virtual IEnumerable<TEntity> AllMatchingBySpec(ISpecification<TEntity> specification)
        {
            var query = AllMatchingQuery(specification);
            return query;
        }

        /// <summary>
        /// Get all elements of type TGenericEntity that matching a
        /// Specification <paramref name="specification"/>
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <returns></returns>
        public virtual IEnumerable<TGenericEntity> AllMatching<TGenericEntity>(
            ISpecification<TGenericEntity> specification) where TGenericEntity : DomainEntity
        {
            var set = DbContext.CreateSet<TGenericEntity>();
            var result = set.Where(specification.IsSatisfied());
            return result;
        }

        /// <summary>
        /// Get all matching with dependants
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="isTracked"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> AllMatchingWithDependendants(ISpecification<TEntity> specification,
            bool isTracked = true)
        {
            return isTracked
                ? GetSetWithDependendants().Where(specification.IsSatisfied())
                : GetSetWithDependendants().Where(specification.IsSatisfied()).AsNoTracking();
        }

        /// <summary>
        /// Gets a list of records for any given data type
        /// </summary>
        /// <typeparam name="TChildEntity"></typeparam>
        /// <param name="specification"></param>
        /// <returns></returns>
        public virtual IEnumerable<TChildEntity> AllMatchingWithNoTracking<TChildEntity>(ISpecification<TChildEntity> specification)
            where TChildEntity : DomainEntity
        {
            var set = DbContext.CreateSet<TChildEntity>();
            return set.Where(specification.IsSatisfied()).AsNoTracking();
        }


        /// <summary>
        /// Check whether there is any TEntity that matching a
        /// Specification <paramref name="specification"/>
        /// </summary>
        /// <param name="specification">Specification that result meet</param>
        /// <returns></returns>
        public bool Exists(ISpecification<TEntity> specification)
        {
            return GetSet().Where(specification.IsSatisfied()).Any();
        }

        public virtual IEnumerable<TEntity> GetPaged<TKProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<TEntity, TKProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        public virtual IEnumerable<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        public virtual void Merge(TEntity persisted, TEntity current)
        {
            DbContext.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region Protected

        protected IDbSet<TEntity> GetSet()
        {
            return DbContext.CreateSet<TEntity>();
        }

        /// <summary>
        /// Get query with fetch strategy
        /// </summary>
        /// <param name="fetchStrategy"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetQuery(IFetchStrategy<TEntity> fetchStrategy)
        {
            IQueryable<TEntity> query = GetSet();

            if (fetchStrategy == null)
            {
                return query;
            }

            query = fetchStrategy.IncludePaths.Aggregate(query, (current, path) => current.Include(path));

            /* Apply order by if applicable */
            if (fetchStrategy.OrderStrategies.Any())
            {
                IOrderedQueryable<TEntity> orderedQueryable = null;
                var cnt = 0;
                foreach (var order in fetchStrategy.OrderStrategies)
                {
                    orderedQueryable = cnt == 0 ? order.ApplyOrderBy(query) : order.ApplyThenBy(orderedQueryable);
                    cnt++;
                }
                query = orderedQueryable;
            }
            // Return query
            return fetchStrategy.IsTracking ? query : query.AsNoTracking();
        }

        /// <summary>
        /// This property can be overridden in child repository classes to get entities with their dependant entities included
        /// </summary>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetSetWithDependendants()
        {
            return GetSet();
        }

        protected IQueryable<TEntity> AllMatchingQuery(ISpecification<TEntity> specification)
        {
            return GetQuery(specification.FetchStrategy).Where(specification.IsSatisfied());
        }

        protected bool IsEmptyKey(TKey key)
        {
            if (key is int)
            {
                return ((key as int?) == 0);
            }
            else if (key is Guid)
            {
                return ((key as Guid?) == Guid.Empty);
            }

            return key == null;
        }

        /// <summary>
        /// Create sql parameter for stored proc
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected SqlParameter CreateSqlParameter(string name, SqlDbType type, object value)
        {
            var paramName = name.StartsWith("@") ? name : "@" + name;
            return new SqlParameter(paramName, type) { Value = value };
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (DbContext != null)
                DbContext.Dispose();
        }

        #endregion

        #region Private Methods



        #endregion
    }
}
