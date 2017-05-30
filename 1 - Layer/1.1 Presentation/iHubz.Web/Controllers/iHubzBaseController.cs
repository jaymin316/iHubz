using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using iHubz.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace iHubz.Web.Controllers
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [Authorize]
    public class IHubzBaseController : Controller
    {
        protected IHubzBaseController()
        {
        }

        public string LoggedOnUser
        {
            get { return User.Identity.Name; }
        }

        public bool IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var context = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = userManager.GetRoles(user.GetUserId());
                return s[0] == "Admin";
            }
            return false;
        }    
    }
}