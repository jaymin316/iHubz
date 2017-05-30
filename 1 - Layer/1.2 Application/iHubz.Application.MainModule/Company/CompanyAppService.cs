using System;
using System.Collections.Generic;
using System.Linq;
using iHubz.Domain.Core;
using iHubz.Domain.MainModule.CompanyEntities;
using iHubz.Domain.MainModule.Repositories;
using iHubz.Domain.MainModule.Specifications;

namespace iHubz.Application.MainModule.Company
{
    public class CompanyAppService : BaseAppService, ICompanyAppService
    {
        #region Members

        private readonly ICompaniesRepository _companiesRepository;
        private static ICompanyPropertiesRepository _companyPropertiesRepository;

        #endregion

        #region Constructors

        public CompanyAppService(ICompaniesRepository companiesRepository, ICompanyPropertiesRepository companyPropertiesRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            if (companiesRepository == null)
                throw new ArgumentNullException("companiesRepository");

            if (companyPropertiesRepository == null)
                throw new ArgumentNullException("companyPropertiesRepository");

            _companiesRepository = companiesRepository;
            _companyPropertiesRepository = companyPropertiesRepository;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            //dispose all resources
            _companiesRepository.Dispose();
            _companyPropertiesRepository.Dispose();
        }

        #endregion

        #region Public methods

        public IEnumerable<Companies> GetAllCompanies(string username)
        {
            var spec = CompaniesSpecFactory.AllCompanies();
            return _companiesRepository.AllMatchingBySpec(spec).OrderBy(x => x.CompanyId);
        }

        public IEnumerable<Companies> GetAllCompaniesWithStates()
        {
            var spec = CompaniesSpecFactory.AllCompaniesWithStates();
            return _companiesRepository.AllMatchingBySpec(spec).OrderBy(x => x.CompanyName);
        }

        public IEnumerable<CompanyProperties> GetCompanyPropertiesById(int companyId)
        {
            var spec = CompanyPropertiesSpecFactory.CompanyPropertiesByCompanyId(companyId);
            return _companyPropertiesRepository.AllMatchingBySpec(spec);
        }

        public void SaveCompany(Companies company, string username)
        {
            company.CreatedBy = username;
            company.CreatedDate = DateTime.Now;
            company.ModifiedBy = username;
            company.ModifiedDate = DateTime.Now;

            // Save company properties
            UpdateProperties(company, username);

            // Save company
            _companiesRepository.Add(company);

            // Save all changes to database
            UnitOfWork.Commit();
        }

        public void UpdateCompany(Companies company, string username)
        {
            company.ModifiedBy = username;
            company.ModifiedDate = DateTime.Now;

            // Update company properties
            UpdateProperties(company, username);

            // Update company 
            _companiesRepository.Modify(company);

            // Update all changes in database
            UnitOfWork.Commit();
        }

        private static void UpdateProperties(Companies company, string username)
        {
            foreach (var companyProperty in company.CompanyProperties)
            {
                companyProperty.CompanyId = company.CompanyId;
             
                companyProperty.ModifiedBy = username;
                companyProperty.ModifiedDate = DateTime.Now;

                // Save if new property
                if (companyProperty.CompanyPropertyId <= 0)
                {
                    companyProperty.CreatedBy = username;
                    companyProperty.CreatedDate = DateTime.Now;
                    _companyPropertiesRepository.Add(companyProperty);
                }
                else // Update if existing property
                    _companyPropertiesRepository.Modify(companyProperty);
            }
        }

        public bool DeleteCompany(int companyId)
        {
            try
            {
                var companyToDelete = _companiesRepository.Get(companyId);
                // Remove property
                CompanyProperties companyPropertyToDelete = companyToDelete.CompanyProperties.FirstOrDefault();
                if (companyPropertyToDelete != null)
                    _companyPropertiesRepository.Remove(companyPropertyToDelete);
                
                // Remove the company
                _companiesRepository.Remove(companyToDelete);

                UnitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
        
    }
}
