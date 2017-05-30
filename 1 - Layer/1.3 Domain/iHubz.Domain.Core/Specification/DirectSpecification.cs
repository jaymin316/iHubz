using System;
using System.Linq.Expressions;

namespace iHubz.Domain.Core.Specification
{
    public sealed class DirectSpecification<TEntity>
       : Specification<TEntity>
       where TEntity : ISpecifiable
    {
        #region Members
        //private static readonly ILogger Log = LoggerFactory.CreateLog(typeof(DirectSpecification<TEntity>));

        Expression<Func<TEntity, bool>> _matchingCriteria;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor for Direct Specification
        /// </summary>
        /// <param name="matchingCriteria">A Matching Criteria</param>
        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("matchingCriteria");

            _matchingCriteria = matchingCriteria;
        }

        #endregion

        #region Override

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<TEntity, bool>> IsSatisfied()
        {
            return _matchingCriteria;
        }

        #endregion

        /// <summary>
        /// Check whether an entity is satisfied
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        public override bool IsSatisfiedBy(TEntity resource)
        {
            var isSatisfied = false;
            try
            {
                isSatisfied = _matchingCriteria.Compile()(resource);
            }
            catch (Exception)
            {
                // Log
                //Log.Error(resource.ToString());
                // Re-throw
                throw;
            }

            return isSatisfied;
        }
    }
}
