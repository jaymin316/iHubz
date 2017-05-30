using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using iHubz.Domain.Core;
using iHubz.Domain.MainModule.CompanyEntities;

namespace iHubz.Domain.MainModule.ReferenceData
{
    public class States : DomainEntity
    {
        #region Properties
        public int StateId { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }

        public virtual ICollection<Companies> Companies { get; set; }
        #endregion
    }
}
