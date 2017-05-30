using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using iHubz.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using EntityState = System.Data.Entity.EntityState;

namespace iHubz.Web.Controllers
{
    public class RoleController : IHubzBaseController
    {
        readonly ApplicationDbContext _context;

        public RoleController()
        {
            _context = new ApplicationDbContext();  
        }

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!IsAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var roles = _context.Roles.ToList();
            return View(roles);

        }

        public ActionResult ManageRoles(IdentityRole model)
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterRole(IdentityRole role)
        {
            var success = false;
            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            
            try
            {
                if (ModelState.IsValid)
                {
                    var existingRole = roleManager.FindById(role.Id);
                    // Check if role exists in database
                    if (existingRole != null)
                    {
                        // Only update if name is changed
                        if (existingRole.Name != role.Name)
                        {
                            existingRole.Name = role.Name;
                            context.Entry(existingRole).State = EntityState.Modified;
                        }
                    }
                    else
                    {
                        // Save new role
                        context.Entry(role).State = EntityState.Added;
                    }

                    context.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("OtherExceptions", "Unknown error - " + ex.Message);
            }

            if (success)
                return RedirectToAction("Index");

            return View("ManageRoles", role);
        }

        public ActionResult Cancel()
        {
            // Cancel and go back to roles screen
            return RedirectToAction("Index");
        }
    }
}