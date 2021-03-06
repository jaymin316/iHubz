﻿using System;
using System.Linq.Expressions;

namespace iHubz.Domain.Core.Specification
{
    public sealed class AndSpecification<T>
       : CompositeSpecification<T>
       where T : ISpecifiable
    {
        #region Members

        private ISpecification<T> _RightSideSpecification = null;
        private ISpecification<T> _LeftSideSpecification = null;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Default constructor for AndSpecification
        /// </summary>
        /// <param name="leftSide">Left side specification</param>
        /// <param name="rightSide">Right side specification</param>
        public AndSpecification(ISpecification<T> leftSide, ISpecification<T> rightSide)
        {
            if (leftSide == (ISpecification<T>)null)
                throw new ArgumentNullException("leftSide");

            if (rightSide == (ISpecification<T>)null)
                throw new ArgumentNullException("rightSide");

            this._LeftSideSpecification = leftSide;
            this._RightSideSpecification = rightSide;
        }

        #endregion

        #region Composite Specification overrides

        /// <summary>
        /// Left side specification
        /// </summary>
        public override ISpecification<T> LeftSideSpecification
        {
            get { return _LeftSideSpecification; }
        }

        /// <summary>
        /// Right side specification
        /// </summary>
        public override ISpecification<T> RightSideSpecification
        {
            get { return _RightSideSpecification; }
        }

        /// <summary>
        /// <see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{T}"/>
        /// </summary>
        /// <returns><see cref="Microsoft.Samples.NLayerApp.Domain.Seedwork.Specification.ISpecification{T}"/></returns>
        public override Expression<Func<T, bool>> IsSatisfied()
        {
            Expression<Func<T, bool>> left = _LeftSideSpecification.IsSatisfied();
            Expression<Func<T, bool>> right = _RightSideSpecification.IsSatisfied();

            return (left.And(right));
        }

        public override bool IsSatisfiedBy(T resource)
        {
            bool leftOK = _LeftSideSpecification.IsSatisfiedBy(resource);
            bool rightOK = _RightSideSpecification.IsSatisfiedBy(resource);
            return leftOK && rightOK;
        }

        #endregion
    }
}
