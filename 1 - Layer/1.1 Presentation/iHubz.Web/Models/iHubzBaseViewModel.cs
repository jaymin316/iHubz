// ReSharper disable InconsistentNaming

namespace iHubz.Web.Models
{
    public class iHubzBaseViewModel
    {
        public bool IsSuccess { get; set; }
        public iHubzBaseViewModel()
        {
            IsSuccess = true;
        }
    }
}