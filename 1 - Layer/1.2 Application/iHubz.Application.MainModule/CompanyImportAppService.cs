using System;
using System.Collections;
using System.Collections.Generic;
using iHubz.Application.MainModule.Company;
using iHubz.Domain.MainModule.CompanyEntities;
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

            IList<Companies> allCompanyChanges = new List<Companies>();

            try
            {
                // Create workbook
                var excelWorkbook = excelManager.CreateWorkbook(actualFileName, fileData);

                // Get worksheet
                var excelWorksheet = excelWorkbook.Sheets[0];

                var totalRows = excelWorksheet.CountDataRow();

                // Start importing data
                for (var i = CompanyConstants.CompanyDetails.DETAILS_ROWINDEX; i < totalRows && !hasUnsafeInput; i++)
                {
                    // Get company name from the row
                    var companyName = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.COMPANY_NAME_COLINDEX).ToString();

                    // Contact person
                    string contact = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.CONTACT_PERSON_COLINDEX) != null)
                        contact = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.CONTACT_PERSON_COLINDEX).ToString();

                    // Products/manufacture
                    var productsManufacture = excelWorksheet.GetCellValue(i,
                                CompanyConstants.CompanyDetails.PRODUCTS_MANUFACTURE_COLINDEX).ToString();

                    // Category
                    string category = null;
                    if (excelWorksheet.GetCellValue(i,CompanyConstants.CompanyDetails.CATEGORY_COLINDEX) != null)
                        category = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.CATEGORY_COLINDEX).ToString();

                    // TODO: Sub-category

                    // Address
                    string address1 = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.ADDRESS_LINE_1_COLINDEX) != null)
                        address1 = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.ADDRESS_LINE_1_COLINDEX).ToString();

                    string address2 = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.ADDRESS_LINE_2_COLINDEX) != null)
                        address2 = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.ADDRESS_LINE_2_COLINDEX).ToString();
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.ADDRESS_LINE_3_COLINDEX) != null)
                        address2 += excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.ADDRESS_LINE_3_COLINDEX).ToString();

                    // Website
                    string website = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.WEBSITE_COLINDEX) != null)
                        website = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.WEBSITE_COLINDEX).ToString();

                    // City
                    string city = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.CITY_COLINDEX) != null)
                        city = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.CITY_COLINDEX).ToString();

                    // Pincode
                    string pincode = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.PINCODE_COLINDEX) != null)
                        pincode = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.PINCODE_COLINDEX).ToString();

                    // Email 1
                    string email1 = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.EMAIL_1_COLINDEX) != null)
                        email1 = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.EMAIL_1_COLINDEX).ToString();

                    // Email 2
                    string email2 = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.EMAIL_2_COLINDEX) != null)
                        email2 = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.EMAIL_2_COLINDEX).ToString();

                    // Mobile
                    string mobile = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.MOBILE_COLINDEX) != null)
                        mobile = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.MOBILE_COLINDEX).ToString();

                    // Work phone
                    string workphone = null;
                    if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.OFFICE_NUMBER_COLINDEX) != null)
                        workphone = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.OFFICE_NUMBER_COLINDEX).ToString();


                    //TODO: Look up state and get id
                    //string state = null;
                    //if (excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.STATE_COLINDEX) != null)
                    //    state = excelWorksheet.GetCellValue(i, CompanyConstants.CompanyDetails.STATE_COLINDEX).ToString();
                    int stateId = 12;

                    if (!companyName.IsSafeInput() || contact.IsSafeInput() || !productsManufacture.IsSafeInput() 
                        || category.IsSafeInput() || address1.IsSafeInput() || address2.IsSafeInput() || website.IsSafeInput() 
                        || city.IsSafeInput() || pincode.IsSafeInput() || email1.IsSafeInput() || email2.IsSafeInput())
                    {
                        hasUnsafeInput = true;
                    }

                    // Set up final company data
                    // Convert all of this new excel imported data to Companies type
                    var newCompany = new Companies
                    {
                        CompanyName = companyName,
                        ContactName1 = contact,
                        //CompanyProperties.Products = productsManufacture,
                        //CompanyProperties.BusinessCategory = category,
                        AddressLine1 = address1,
                        AddressLine2 = address2,
                        Website = website,
                        City = city,
                        Pincode = pincode,
                        StateId = stateId,
                        Email1 = email1,
                        Email2 = email2,
                        Mobile = mobile,
                        WorkPhone1 = workphone
                    };

                    // Add converted excel data to IList<Companies>
                    allCompanyChanges.Add(newCompany);

                } // End for loop - end import data

                // 3. Send this list to repo. 
                // 4. In repo, Convert this list to a ChangeSet and send to stored proc for bulk insert
                //if (!hasUnsafeInput)
                //{
                //    _coaWebsheetRepository.SaveWebsheetTranslations(translationLanguage, ownerId,
                //        allAccountTranslations,
                //        allColGrpTranslations, allColumnTranslations, username);
                //    result = true;
                //}
            }
            catch (Exception ex)
            {
                result = false;
            }

            if (hasUnsafeInput)
                throw new Exception("Value contains invalid characters");

            return result;
        }
    }
}
