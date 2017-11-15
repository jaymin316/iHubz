using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using iHubz.Domain.MainModule.CompanyEntities;
using iHubz.Domain.MainModule.Repositories;
using iHubz.Infrastructure.Data.Core;
using iHubz.Infrastructure.Data.MainModule.DataSet;
using MainModuleUnitOfWork = iHubz.Infrastructure.Data.MainModule.UnitOfWork.MainModuleUnitOfWork;

namespace iHubz.Infrastructure.Data.MainModule.Repositories
{
    public class CompaniesRepository : Repository<Companies, int>, ICompaniesRepository
    {
        #region Constructor

        public CompaniesRepository(MainModuleUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #endregion

        public bool DeleteCompanyById(int companyId)
        {
            var currentUnitOfWork = this.UnitOfWork as MainModuleUnitOfWork;
            var companiesToDelete = currentUnitOfWork.Companies.Where(n => n.CompanyId == companyId);
            if (companiesToDelete.Any())
            {
                try
                {
                    // Remove all the companies from the collection 
                    foreach (Companies companyToDelete in companiesToDelete)
                    {
                        Remove(companyToDelete);
                    }
                    return true; // successfully removed
                }
                catch (Exception)
                {
                    return false; // delete failed
                }
            }
            return false;
        }

        public void SaveImportedCompanies(IEnumerable<Companies> allCompanyChanges, string username)
        {
            /* Convert updated entities into data tables */
            var allCompaniesChangeSetData = ChangeSetDataSet.ConvertToDataTable(allCompanyChanges);

            /* Call stored procedure to do bulk insert/update of data */ 
            DbContext.ExecuteStoredProcNonQuery("usp_CompaniesSave",
                new SqlParameter("@Username", username),
                CreateSqlParameter("AllCompaniesData", SqlDbType.Structured, allCompaniesChangeSetData)
            );
        }
    }
}
