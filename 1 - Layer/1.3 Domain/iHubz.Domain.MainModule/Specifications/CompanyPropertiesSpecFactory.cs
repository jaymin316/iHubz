using iHubz.Domain.Core.FetchStrategy;
using iHubz.Domain.Core.Specification;
using iHubz.Domain.MainModule.CompanyEntities;

namespace iHubz.Domain.MainModule.Specifications
{
    public static class CompanyPropertiesSpecFactory
    {
        public static ISpecification<CompanyProperties> CompanyPropertiesByCompanyId(int companyId)
        {
            var fetchStrategy = new GenericFetchStrategy<CompanyProperties>
            {
                IsTracking = false
            };
            var spec =
                new DirectSpecification<CompanyProperties>(t => t.CompanyId == companyId)
                {
                    FetchStrategy = fetchStrategy
                };
            return spec;
        }
    }
}
