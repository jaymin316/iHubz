using System.Collections.Generic;
using iHubz.Domain.MainModule.CompanyEntities;

namespace iHubz.Infrastructure.Data.MainModule.DataSet
{
    public partial class ChangeSetDataSet
    {
        public static CompanyChangeSetDataTable ConvertToDataTable(IEnumerable<Companies> allCompanies)
        {
            var companyChanges = new CompanyChangeSetDataTable();
            foreach (var company in allCompanies)
            {
                var newRow = companyChanges.NewCompanyChangeSetRow();

                newRow.CompanyName = company.CompanyName;
                newRow.ContactPerson = company.ContactName1;
                //newRow.Products = company.CompanyProperties.Products;
                //newRow.BusinessCategory = company.CompanyProperties.Category;
                newRow.Address1 = company.AddressLine1;
                newRow.Address2 = company.AddressLine2;
                newRow.Website = company.Website;
                newRow.City = company.City;
                newRow.Pincode = company.Pincode;
                //newRow.State = company.StateId;
                newRow.Email1 = company.Email1;
                newRow.Email2 = company.Email2;
                newRow.Mobile = company.Mobile;
                newRow.WorkPhone1 = company.WorkPhone1;

                companyChanges.AddCompanyChangeSetRow(newRow);
            }

            return companyChanges;
        }
    }
}
