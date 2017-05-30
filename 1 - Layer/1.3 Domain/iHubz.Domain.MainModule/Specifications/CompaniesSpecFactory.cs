using System;
using iHubz.Domain.Core.Specification;
using iHubz.Domain.MainModule.CompanyEntities;
using iHubz.Domain.MainModule.CompanyEntities.FetchStrategy;

namespace iHubz.Domain.MainModule.Specifications
{
    public static class CompaniesSpecFactory
    {
        public static ISpecification<Companies> AllCompanies()
        {
            return new DirectSpecification<Companies>(c => c.CreatedBy == "");
        }

        public static ISpecification<Companies> AllCompaniesWithStates()
        {
            var spec = new DirectSpecification<Companies>(c => !String.IsNullOrEmpty(c.CompanyName));
            spec.FetchStrategy = new CompanyFetchStrategy(false)
            {
                IncludeStates = true,
                IncludeProperties = true
            };

            return spec;
        }
    }
}
