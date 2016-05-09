using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Logios.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Trophy> Trophies { get; set; }
        public DbSet<UserTrophy> UserTrophies { get; set; }
        public DbSet<UserExercise> UserExercise { get; set; }
        public DbSet<TopicArea> TopicAreas { get; set; }
        public DbSet<TopicAreaTopic> TopicAreaTopics { get; set; }
    }
}