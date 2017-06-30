using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using iHubz.Application.MainModule.Company;
using iHubz.Application.MainModule.State;
using iHubz.Domain.MainModule.CompanyEntities;
using iHubz.Domain.MainModule.ReferenceData;
using iHubz.Infrastructure.CrossCutting.Extensions;
using iHubz.Web.Models;
using PagedList;

namespace iHubz.Web.Controllers
{
    public class CompaniesController : IHubzBaseController
    {
        private readonly ICompanyAppService _companyAppService;
        private readonly IStateAppService _stateAppService;

        public CompaniesController(ICompanyAppService companyAppService, IStateAppService stateAppService)
        {
            _companyAppService = companyAppService;
            _stateAppService = stateAppService;
        }

        /// <summary>
        /// Ajax from SearchCompanies view calls this function 
        /// </summary>
        /// <returns>return json object for States dropdown</returns>
        public JsonResult GetStates()
        {
            // Get list of states from database
            var statesList = _stateAppService.GetAllStates().ToList();
            // Add a default state entry
            statesList.Insert(0, new States {StateId= 0, StateName = "--- Please Select ---"}); 

            // Convert statesList to JSON and return to view
            var resultData = statesList.Select(s => new { Value = s.StateId, Text = s.StateName });
            return Json(new { result = resultData }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Called when clicking on View company (option from Companies menu)
        /// Displays a data-table of all existing companies from the database
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult ViewCompanies(string sortOrder, string CurrentSort, int? page)
        {
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var allCompanies = _companyAppService.GetAllCompaniesWithStates().ToList();

            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? CompanyConstants.DEFAULT_VIEW_COMPANY_SORT_ORDER : sortOrder;

            // Apply paging to the data table
            var companiesDataTable = CreatePagedCompaniesDataTable(sortOrder, CurrentSort, allCompanies, pageIndex);

            return View(companiesDataTable);
        }

        /// <summary>
        /// Called when clicking on Search company (option from Companies menu)
        /// </summary>
        /// <param name="sortOrder">Sort order: Ascending or Descending</param>
        /// <param name="CurrentSort">Column to sort on</param>
        /// <param name="page">Page number</param>
        /// <param name="txtSearchName">Company name to search on</param>
        /// <param name="txtSearchWebsite">Website to search on</param>
        /// <param name="txtSearchCity">City to search on</param>
        /// <param name="txtSearchDistrict">District to search on</param>
        /// <param name="txtSearchPincode">Pincode to search on</param>
        /// <param name="drpSearchState">State to search on</param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult SearchCompanies(string sortOrder, string CurrentSort, int? page, string txtSearchName,
            string txtSearchWebsite, string txtSearchCity, string txtSearchDistrict, string txtSearchPincode, string drpSearchState)
        {
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var allCompanies = _companyAppService.GetAllCompaniesWithStates();

            ViewBag.CurrentSort = sortOrder;

            sortOrder = String.IsNullOrEmpty(sortOrder) ? CompanyConstants.DEFAULT_VIEW_COMPANY_SORT_ORDER : sortOrder;

            var stateId = Convert.ToInt32(drpSearchState);
            // Apply search filters
            allCompanies = FilterAllCompanies(txtSearchName, txtSearchWebsite, txtSearchCity, txtSearchDistrict, 
                txtSearchPincode, stateId, allCompanies);

            // Apply paging to the resulting data table
            var companiesDataTable = CreatePagedCompaniesDataTable(sortOrder, CurrentSort, allCompanies, pageIndex);
           
            return View(companiesDataTable);
        }

        /// <summary>
        /// Pagination of data table so that it is more readable 
        /// </summary>
        /// <param name="sortOrder">Sort order: Ascending or Descending</param>
        /// <param name="currentSort">Column to sort on</param>
        /// <param name="allCompanies">List of companies on which we will apply paging</param>
        /// <param name="pageIndex">Current page number</param>
        /// <returns></returns>
        private static IPagedList<Companies> CreatePagedCompaniesDataTable(string sortOrder, string currentSort,
            IEnumerable<Companies> allCompanies, int pageIndex)
        {
            var pageSize = CompanyConstants.PAGE_SIZE;
            IPagedList<Companies> dataTable = null;

            switch (sortOrder)
            {
                case "CompanyName":
                    dataTable = sortOrder.Equals(currentSort)
                        ? allCompanies.OrderByDescending(c => c.CompanyName).ToPagedList(pageIndex, pageSize)
                        : allCompanies.OrderBy(c => c.CompanyName).ToPagedList(pageIndex, pageSize);
                    break;
                case "Website":
                    dataTable = sortOrder.Equals(currentSort)
                        ? allCompanies.OrderByDescending(c => c.Website).ToPagedList(pageIndex, pageSize)
                        : allCompanies.OrderBy(c => c.Website).ToPagedList(pageIndex, pageSize);
                    break;
                case "City":
                    dataTable = sortOrder.Equals(currentSort)
                        ? allCompanies.OrderByDescending(c => c.City).ToPagedList(pageIndex, pageSize)
                        : allCompanies.OrderBy(c => c.City).ToPagedList(pageIndex, pageSize);
                    break;
                case "State":
                    dataTable = sortOrder.Equals(currentSort)
                        ? allCompanies.OrderByDescending(c => c.State.StateName).ToPagedList(pageIndex, pageSize)
                        : allCompanies.OrderBy(c => c.State.StateName).ToPagedList(pageIndex, pageSize);
                    break;
                case "Default":
                    dataTable = allCompanies.OrderBy(c => c.CompanyName).ToPagedList(pageIndex, pageSize);
                    break;
            }
            return dataTable;
        }

        /// <summary>
        /// Filters the companies list based on user entered search criteria
        /// </summary>
        /// <param name="txtSearchName">Company name to search</param>
        /// <param name="txtSearchWebsite">Website to search</param>
        /// <param name="txtSearchCity">City to search</param>
        /// <param name="txtSearchDistrict">District to search</param>
        /// <param name="txtSearchPincode">Pincode to search</param>
        /// <param name="stateId">StateId to search</param>
        /// <param name="allCompanies">List of companies on which we will apply search filters</param>
        /// <returns></returns>
        private static IEnumerable<Companies> FilterAllCompanies(string txtSearchName, string txtSearchWebsite,
            string txtSearchCity, string txtSearchDistrict, string txtSearchPincode, int stateId,
            IEnumerable<Companies> allCompanies)
        {
            allCompanies = allCompanies.Where(c =>
                    (String.IsNullOrEmpty(txtSearchName) || CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                         c.CompanyName, txtSearchName, CompareOptions.IgnoreCase) >= 0)
                    &&
                    (String.IsNullOrEmpty(txtSearchWebsite) || CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                         c.Website, txtSearchWebsite, CompareOptions.IgnoreCase) >= 0)
                    &&
                    (String.IsNullOrEmpty(txtSearchCity) || CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                         c.City, txtSearchCity, CompareOptions.IgnoreCase) >= 0)
                    &&
                    (String.IsNullOrEmpty(txtSearchDistrict) || CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                         c.District, txtSearchDistrict, CompareOptions.IgnoreCase) >= 0)
                    &&
                    (String.IsNullOrEmpty(txtSearchPincode) || CultureInfo.CurrentCulture.CompareInfo.IndexOf(
                         c.Pincode, txtSearchPincode, CompareOptions.IgnoreCase) >= 0)
                    &&
                    (stateId == 0 || c.StateId == stateId)
                ).ToList()
                .OrderBy(c => c.CompanyName);

            return allCompanies;
        }

        /// <summary>
        /// GET.. called when clicking on ManageCompnay.
        /// Displays the ManageCompany view so that we can insert data into the textboxes to save/update
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageCompany(CompaniesViewModel model)
        {
            SetStates(model);

            // Get properties (if they exist)
            var cmpProperties = _companyAppService.GetCompanyPropertiesById(model.CompanyId).ToList();
            if (cmpProperties.Any())
                model.CompanyProperties = cmpProperties;

            // Add default property option when one doesn't exist
            if (model.CompanyProperties.Count == 0)
            {
                var defaultCompanyProperty = new CompanyProperties();
                model.CompanyProperties.Add(defaultCompanyProperty);
            }

            ModelState.Clear();
            return View(model);
        }

        /// <summary>
        /// POST... Called when clicking on SAVE from ManageCompany
        /// Saves the data entered on the ManageCompany form
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RegisterCompany(CompaniesViewModel company)
        {
            var success = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var companyObj = company.ProjectedAs<Companies>();
                    if (companyObj.CompanyId > 0)
                        _companyAppService.UpdateCompany(companyObj, LoggedOnUser);
                    else
                        _companyAppService.SaveCompany(companyObj, LoggedOnUser);

                    success = true;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("OtherExceptions", @"Unknown error - " + ex.Message);
            }

            if (success)
                return RedirectToAction("ViewCompanies");

            SetStates(company);
            return View("ManageCompany", company);
        }

        public ActionResult Cancel()
        {
            return RedirectToAction("ViewCompanies");
        }

        public ActionResult DeleteCompany(int companyId)
        {
            _companyAppService.DeleteCompany(companyId);
            return RedirectToAction("ViewCompanies");
        }

        #region Private methods
        private void SetStates(CompaniesViewModel model)
        {
            var statesList = _stateAppService.GetAllStates();
            model.States = GetSelectListItems(statesList);
        }

        private IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<States> statesList)
        {
            return statesList.Select(element => new SelectListItem
            {
                Value = element.StateId.ToString(),
                Text = element.StateName
            }).ToList();
        }
        #endregion

    }
}