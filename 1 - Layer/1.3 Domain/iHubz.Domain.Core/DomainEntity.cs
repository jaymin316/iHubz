using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iHubz.Domain.Core
{
    public abstract class DomainEntity : IValidatableObject, ISpecifiable
    {
        #region IValidatableObject Members

        /// <summary>
        /// Validate entity data
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            return validationResults;
        }
        #endregion
    }
}
