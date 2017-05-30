using System.ComponentModel.DataAnnotations;
using iHubz.Domain.Core;

namespace iHubz.Domain.MainModule.CompanyEntities
{
    public class CompanyProperties : DomainEntity
    {
        public int CompanyPropertyId { get; set; }
        public int CompanyId { get; set; }
        [Display(Name = "Company Capacity")]
        public int? CompanyCapacity { get; set; }
        [Display(Name = "AMC (Annual Maintenance Contract)")]
        public bool AnnualMaintenanceContract { get; set; }
        [Display(Name = "Main Business Category")]
        public string BusinessCategory { get; set; }
        [Display(Name = "Business Sub-Category 1")]
        public string BusinessCategory1 { get; set; }
        [Display(Name = "Business Sub-Category 2")]
        public string BusinessCategory2 { get; set; }
        [Display(Name = "Business Sub-Category 3")]
        public string BusinessCategory3 { get; set; }
        [Display(Name = "Business Sub-Category 4")]
        public string BusinessCategory4 { get; set; }
        [Display(Name = "Business Sub-Category 5")]
        public string BusinessCategory5 { get; set; }
        [Display(Name = "Business Sub-Category 6")]
        public string BusinessCategory6 { get; set; }
        [Display(Name = "Business Sub-Category 7")]
        public string BusinessCategory7 { get; set; }
        [Display(Name = "Business Sub-Category 8")]
        public string BusinessCategory8 { get; set; }
        [Display(Name = "Business Sub-Category 9")]
        public string BusinessCategory9 { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        public Companies Company { get; set; }
    }
}
