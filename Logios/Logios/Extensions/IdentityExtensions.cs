using Logios.Entities;
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

                    
                    points += context.UserProfiles.Find(userId).Points;

                return points;
            }
            
            
        }

        

    }
}