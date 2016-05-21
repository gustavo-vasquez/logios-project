using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logios.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Logios.Migrations.DataGenerators
{
    public class ApplicationUserGenerator : IDataGenerator
    {
        public void GenerateData(ApplicationDbContext context)
        {

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var userStore = new RoleStore<IdentityRole>(context);
                var userManager = new RoleManager<IdentityRole>(userStore);
                var roleManager = new IdentityRole { Name = "Admin" };
                userManager.Create(roleManager);
            }

            if (!(context.Users.Any(u => u.UserName == "ADMIN")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "ADMIN", Email = "administrator@example.com" };
                userManager.Create(userToInsert, "Admin12$");
                userManager.AddToRole(userToInsert.Id, "Admin");
            }
           
                context.SaveChanges();

        }
    }
}