using System;
using iHubz.Application.MainModule.Company;
using iHubz.Infrastructure.CrossCutting.Excel;
using iHubz.Infrastructure.CrossCutting.Extensions;

namespace iHubz.Application.MainModule
{
    public class CompanyImportAppService : ICompanyImportAppService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actualFileName"></param>
        /// <param name="fileData"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool PerformImport(string actualFileName, byte[] fileData, string username)
        {
            var excelManager = ExcelManagerFactory.CreateExcelManager();

            var result = false;
            var hasUnsafeInput = false;

            try
            {
                // Create workbook
                var excelWorkbook = excelManager.CreateWorkbook(actualFileName, fileData);

                // Get worksheet
                var excelWorksheet = excelWorkbook.Sheets[0];

                var totalRows = excelWorksheet.CountDataRow();

                // Start importing data
                for (var i = CompanyConstants.CompanyDetails.DETAILS_ROWINDEX;
                    i < totalRows && !hasUnsafeInput;
                    i++)
                {
                    // Get company name from the row
                    var companyName = excelWorksheet.GetCellValue(i,
                                CompanyConstants.CompanyDetails.COMPANY_NAME_COLINDEX).ToString();

                    // Get company name from the row
                    var addressLine1 = excelWorksheet.GetCellValue(i,
                                CompanyConstants.CompanyDetails.ADDRESS_LINE_1_COLINDEX).ToString();

                    if (!companyName.IsSafeInput() || !addressLine1.IsSafeInput())
                    {
                        hasUnsafeInput = true;
                    }

                    // Set up final company data

                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
