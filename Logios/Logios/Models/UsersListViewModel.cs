using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class UsersListViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }        
    }
}