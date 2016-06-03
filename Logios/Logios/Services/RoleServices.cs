using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Services
{
    public class RoleServices
    {
        public bool RoleExist(string name)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Roles.FirstOrDefault(r => r.Name.ToLower() == name.ToLower()) == null)
                {
                    return false;
                }

                return true;
            }            
        }

        public bool CanDeleteRole(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                try
                {
                    context.Database.SqlQuery<int>("Select 1 from AspNetUserRoles where RoleId='" + id + "'").First<int>();
                }
                catch
                {
                    return true;
                }

                return false;
            }
        }
    }
}