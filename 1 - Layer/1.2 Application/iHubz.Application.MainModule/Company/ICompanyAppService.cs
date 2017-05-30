using System.Collections.Generic;
using iHubz.Domain.MainModule.CompanyEntities;

namespace iHubz.Application.MainModule.Company
{
    public interface ICompanyAppService
    {
        IEnumerable<Companies> GetAllCompanies(string username);
        IEnumerable<Companies> GetAllCompaniesWithStates();
        IEnumerable<CompanyProperties> GetCompanyPropertiesById(int companyId);
        void SaveCompany(Companies company, string username);
        void UpdateCompany(Companies company, string username);
        bool DeleteCompany(int companyId);
    }
}
