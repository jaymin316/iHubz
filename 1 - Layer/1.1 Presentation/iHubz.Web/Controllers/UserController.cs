using System.Web.Mvc;

namespace iHubz.Web.Controllers
{
    public class UserController : IHubzBaseController
    {
        // GET: User
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";

                    //return RedirectToAction("Users", "Account");
                }
                return View();
            }
            
            ViewBag.Name = "Not Logged IN";
            return View();    
        }

        //public ActionResult Search(string sortOrder, string CurrentSort, int? page)
        //{
        //    var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
        //    var allUsers = UserManager.Users.ToList();
        //    //ViewBag.Name = new SelectList(_context.Roles.Where(u => !u.Name.Contains("Admin"))
        //    //    .ToList(), "Name", "Name");


        //    ViewBag.CurrentSort = sortOrder;
        //    sortOrder = String.IsNullOrEmpty(sortOrder) ? "FirstName" : sortOrder;

        //    // Apply paging to the data table
        //    var usersDataTable = CreatePagedUsersDataTable(sortOrder, CurrentSort, allUsers, pageIndex);

        //    return View(usersDataTable);
        //}

        //private static IPagedList<ApplicationUser> CreatePagedUsersDataTable(string sortOrder, string currentSort,
        //    IEnumerable<ApplicationUser> allUsers, int pageIndex)
        //{
        //    //var pageSize = CompanyConstants.PAGE_SIZE;
        //    var pageSize = 10;

        //    IPagedList<ApplicationUser> dataTable = null;

        //    switch (sortOrder)
        //    {
        //        case "FirstName":
        //            dataTable = sortOrder.Equals(currentSort)
        //                ? allUsers.OrderByDescending(u => u.FirstName).ToPagedList(pageIndex, pageSize)
        //                : allUsers.OrderBy(u => u.FirstName).ToPagedList(pageIndex, pageSize);
        //            break;
        //        case "Default":
        //            dataTable = allUsers.OrderBy(u => u.FirstName).ToPagedList(pageIndex, pageSize);
        //            break;
        //    }
        //    return dataTable;
        //}
    }
}