using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Services
{
    public class RegisterServices
    {
        public bool UserNameExists(string userName)
        {
            using (var context = new ApplicationDbContext())
            {
                if (context.Users.FirstOrDefault(u => u.UserName == userName) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}