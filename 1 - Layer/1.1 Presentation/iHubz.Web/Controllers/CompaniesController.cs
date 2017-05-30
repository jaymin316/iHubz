using System;
using System.Collections.Generic;
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
        /// Called when clicking on View company (Company option from the menu)
        /// Displays a data-table of all existing companies from the database
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        public ActionResult ViewCompanies(string sortOrder, string CurrentSort, int? page)
        {
            var pageSize = CompanyConstants.PAGE_SIZE;
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var allCompanies = _companyAppService.GetAllCompaniesWithStates().ToList();

            ViewBag.CurrentSort = sortOrder;

            sortOrder = String.IsNullOrEmpty(sortOrder) ? CompanyConstants.DEFAULT_VIEW_COMPANY_SORT_ORDER : sortOrder;

            IPagedList<Companies> companiesDataTable = null;
            switch (sortOrder)
            {
                case "CompanyName":
                    companiesDataTable = sortOrder.Equals(CurrentSort) ?
                        allCompanies.OrderByDescending(c => c.CompanyName).ToPagedList(pageIndex, pageSize) :
                        allCompanies.OrderBy(c => c.CompanyName).ToPagedList(pageIndex, pageSize);
                    break;
                case "Website":
                    companiesDataTable = sortOrder.Equals(CurrentSort) ?
                        allCompanies.OrderByDescending(c => c.Website).ToPagedList(pageIndex, pageSize) :
                        allCompanies.OrderBy(c => c.Website).ToPagedList(pageIndex, pageSize);
                    break;
                case "City":
                    companiesDataTable = sortOrder.Equals(CurrentSort) ?
                        allCompanies.OrderByDescending(c => c.City).ToPagedList(pageIndex, pageSize) :
                        allCompanies.OrderBy(c => c.City).ToPagedList(pageIndex, pageSize);
                    break;
                case "State":
                    companiesDataTable = sortOrder.Equals(CurrentSort) ?
                        allCompanies.OrderByDescending(c => c.State.StateName).ToPagedList(pageIndex, pageSize) :
                        allCompanies.OrderBy(c => c.State.StateName).ToPagedList(pageIndex, pageSize);
                    break;
                case "Default":
                    companiesDataTable = allCompanies.OrderBy(c => c.CompanyName).ToPagedList(pageIndex, pageSize);
                    break;
            }

            return View(companiesDataTable);
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
                ModelState.AddModelError("OtherExceptions", "Unknown error - " + ex.Message);
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