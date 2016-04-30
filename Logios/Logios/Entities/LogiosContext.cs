using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Logios.Entities
{
    public class LogiosContext : DbContext
    {
        public LogiosContext()
                : base("DefaultConnection")
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Topic> Topics { get; set; }
    }
}