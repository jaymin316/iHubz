using iHubz.Web;
using iHubz.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace iHubz.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //CreateRolesandUsers();    
        }

        // Create default User roles and Admin user for login    
        private void CreateRolesandUsers()
        {
            var context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // Create first Admin Role and set myself as admin
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role    
                var role = new IdentityRole {Name = "Admin"};
                roleManager.Create(role);

                var user = userManager.FindByEmail("jaymin_316@yahoo.com");

                //Add default User to Role Admin    
                if (user != null)
                {
                    var result1 = userManager.AddToRole(user.Id, "Admin");
                }
            }

            // Creating Licensed_Company role     
            if (!roleManager.RoleExists("Licensed_Company"))
            {
                var role = new IdentityRole { Name = "Licensed_Company" };
                roleManager.Create(role);

            }

            // Creating Super_Admin role     
            if (!roleManager.RoleExists("Super_Admin"))
            {
                var role = new IdentityRole { Name = "Super_Admin" };
                roleManager.Create(role);

            }
        }  
    }
}
