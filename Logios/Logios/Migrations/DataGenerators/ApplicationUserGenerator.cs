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

            if (!context.Roles.Any(r => r.Name == "Usuario"))
            {
                var userStore = new RoleStore<IdentityRole>(context);
                var userManager = new RoleManager<IdentityRole>(userStore);
                var roleManager = new IdentityRole { Name = "Usuario" };
                userManager.Create(roleManager);
            }

            if (!(context.Users.Any(u => u.UserName == "Administrador")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "Administrador", Email = "team.logios.project@gmail.com" };
                var userProfile = new UserProfile { UserID = userToInsert.Id, Points = 0 };
                userManager.Create(userToInsert, "Admin123<");
                userManager.AddToRole(userToInsert.Id, "Admin");                
                context.UserProfiles.Add(userProfile);
            }            

            if (!(context.Users.Any(u => u.UserName == "Invitado")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser { UserName = "Invitado", Email = "invitado@testing.com" };
                var userProfile = new UserProfile { UserID = userToInsert.Id, Points = 0 };
                userManager.Create(userToInsert, "Invitado99<");
                userManager.AddToRole(userToInsert.Id, "Usuario");
                context.UserProfiles.Add(userProfile);
            }

            context.SaveChanges();

        }
    }
}