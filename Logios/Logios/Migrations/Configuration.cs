namespace Logios.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Logios.Entities.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Logios.Entities.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            var newTopics = new List<Topic>
            {
                new Topic { Description = "Derivadas" },
                new Topic { Description = "Polinomios" }
            };

            if (context.Topics.Count() == 0)
            {
                newTopics.ForEach(t => context.Topics.AddOrUpdate(t));
                context.SaveChanges();
            }

            if (context.Exercises.Count() == 0)
            {
                var newExercises = new List<Exercise>()
                {
                    new Exercise { Problem = "1+1", Development = "1+1=2", Solution = "2", Topic = newTopics[0]  },
                    new Exercise { Problem = "2+2", Development = "2+2=4", Solution = "4", Topic = newTopics[0] },
                    new Exercise { Problem = "2+2+2", Development = "2+2+2=6", Solution = "6", Topic = newTopics[1] }
                };

                newExercises.ForEach(e => context.Exercises.AddOrUpdate(e));
                context.SaveChanges();
            }

            if (context.Trophies.Count() == 0)
            {
                var trophies = new List<Trophy>()
                {
                    new Trophy { Description = "Primer Trofeo", Points = 20 }
                };

                trophies.ForEach(t => context.Trophies.Add(t));
                context.SaveChanges();
            }
        }
    }
}
