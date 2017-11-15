using System.Collections.Generic;
using iHubz.Domain.Core;
using iHubz.Domain.MainModule.CompanyEntities;

namespace iHubz.Domain.MainModule.Repositories
{
    public interface ICompaniesRepository : IRepository<Companies, int>
    {
        bool DeleteCompanyById(int companyId);
        void SaveImportedCompanies(IEnumerable<Companies> allCompanyChanges, string username);
    }
}
