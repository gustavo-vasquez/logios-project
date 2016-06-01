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
                if (context.Roles.FirstOrDefault(r => r.Name == name) == null)
                {
                    return false;
                }

                return true;
            }            
        }
    }
}