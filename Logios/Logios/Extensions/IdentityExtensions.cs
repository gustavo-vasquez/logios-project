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
        //public static string GetCurrentPoints(this IIdentity identity)
        //{
        //    var claim = ((ClaimsIdentity)identity).FindFirst("Points");
        //    return (claim != null) ? claim.Value : string.Empty;
        //}

        public static int GetCurrentPoints(this IIdentity identity, string userId)
        {
            using (var context =new ApplicationDbContext())
            {
                var points = context.UserProfiles.Find(userId).Points;
                return points;
            }
        }

    }
}