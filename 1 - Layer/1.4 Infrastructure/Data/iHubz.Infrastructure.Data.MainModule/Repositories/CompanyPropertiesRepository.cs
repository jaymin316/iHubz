using iHubz.Domain.MainModule.CompanyEntities;
using iHubz.Domain.MainModule.Repositories;
using iHubz.Infrastructure.Data.Core;

namespace iHubz.Infrastructure.Data.MainModule.Repositories
{
    public class CompanyPropertiesRepository : Repository<CompanyProperties, int>, ICompanyPropertiesRepository
    {
        public CompanyPropertiesRepository(IQueryableUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
