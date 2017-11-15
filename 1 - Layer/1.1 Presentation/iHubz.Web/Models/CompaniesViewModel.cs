using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using iHubz.Domain.MainModule.CompanyEntities;

namespace iHubz.Web.Models
{
    //public class CompaniesListViewModel : iHubzBaseViewModel
    //{
    //    public List<CompaniesViewModel> Companies { get; set; }

    //    public CompaniesListViewModel()
    //    {
    //        Companies = new List<CompaniesViewModel>();
    //    }
    //}

    public class CompaniesViewModel
    {
        #region Constructors
        public CompaniesViewModel()
        {
            CompanyProperties = new List<CompanyProperties>();
        }
        #endregion

        public int CompanyId { get; set; }
        [Required(ErrorMessage = "Please enter company name.")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public string Website { get; set; }

        [Required(ErrorMessage = "Please enter Address.")]
        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Please enter City.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select a State.")]
        [Display(Name = "State")]
        public int StateId { get; set; }

        [Required(ErrorMessage = "Please enter District.")]
        public string District { get; set; }
        
        [Required(ErrorMessage = "Please enter Pincode.")]
        public string Pincode { get; set; }

        [Display(Name = "Contact 1 Name")]
        public string ContactName1 { get; set; }

        [Display(Name = "Email")]
        public string Email1 { get; set; }

        [Display(Name = "Work Phone")]
        public string WorkPhone1 { get; set; }

        [Display(Name = "Contact 2 Name")]
        public string ContactName2 { get; set; }

        [Display(Name = "Email")]
        public string Email2 { get; set; }

        [Display(Name = "Work Phone")]
        public string WorkPhone2 { get; set; }

        public string Mobile { get; set; }

        public List<CompanyProperties> CompanyProperties { get; set; }
        public virtual IEnumerable<SelectListItem> States { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }

    }

    public class ImportCompaniesModel
    {
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Choose File")]
        public HttpPostedFileBase FileUpload { get; set; }
    }
}