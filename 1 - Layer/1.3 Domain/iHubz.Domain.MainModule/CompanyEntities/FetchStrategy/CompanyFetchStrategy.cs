using System.Collections.Generic;
using iHubz.Domain.Core.FetchStrategy;

namespace iHubz.Domain.MainModule.CompanyEntities.FetchStrategy
{
    public class CompanyFetchStrategy : GenericFetchStrategy<Companies>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyFetchStrategy(bool isTracking) : base(isTracking)
        {
            //Include(c => c.States);
        }

        #region Properties

        public bool IncludeStates { get; set; }
        public bool IncludeProperties { get; set; }

        #endregion

        #region Overrides
        public override IEnumerable<string> IncludePaths
        {
            get
            {
                if (IncludeStates)
                    Include(i => i.State);

                if (IncludeProperties)
                    Include(i => i.CompanyProperties);

                return base.IncludePaths;
            }
        }

        #endregion
    }
}
