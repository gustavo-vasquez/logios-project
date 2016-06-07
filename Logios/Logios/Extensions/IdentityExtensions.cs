using Logios.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Logios.Extensions
{
    public static class IdentityExtensions 
    {
        public static string GetUserEmail(this IIdentity identity,string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Users.Find(userId).Email;
            }
            
        }

        public static int GetCurrentPoints(this IIdentity identity, string userId)
        {
            
            using (var context =new ApplicationDbContext())
            {
                var points = 0;
                var userTrophiesPoints = context.UserTrophies
                                                        .Where(x => x.UserId == userId)
                                                        .Select(x => x.Trophy.Points)
                                                        .ToList();

                foreach (var trophyPoints in userTrophiesPoints)
                    {
                        points += trophyPoints;
                    }

                try
                {
                    points += context.UserProfiles.Find(userId).Points;
                }
                catch
                {
                    var userProfile = new UserProfile { UserID = userId, Points = 0, ImagePath = "1465327785_profle.png" };
                    context.UserProfiles.Add(userProfile);
                    context.SaveChanges();
                    var userStore = new UserStore<ApplicationUser>(context);
                    var userManager = new UserManager<ApplicationUser>(userStore);
                    userManager.AddToRole(userId, "Usuario");
                    points += context.UserProfiles.Find(userId).Points;                    
                }

                return points;
            }
            
            
        }

        

    }
}