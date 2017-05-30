using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using iHubz.Domain.Core;
using iHubz.Domain.MainModule.ReferenceData;

namespace iHubz.Domain.MainModule.CompanyEntities
{
    public class Companies : DomainEntity
    {
        public int CompanyId { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public string Website { get; set; }

        [Display(Name = "Address Line 1")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string AddressLine2 { get; set; }

        public string City { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        public string District { get; set; }
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

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<CompanyProperties> CompanyProperties { get; set; }
        public virtual States State { get; set; }
    }
}
