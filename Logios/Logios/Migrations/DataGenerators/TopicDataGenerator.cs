using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Logios.Entities;

namespace Logios.Migrations.DataGenerators
{
    public class TopicDataGenerator : IDataGenerator
    {
        public void GenerateData(ApplicationDbContext context)
        {
            var newTopics = new List<Topic>
            {
                new Topic { TopicId = 1, Description = "Derivadas" },
                new Topic { TopicId = 2, Description = "Polinomios" },
                new Topic { TopicId = 3, Description = "Integrales" }
            };

            newTopics.ForEach(t => context.Topics.Add(t));
            context.SaveChanges();
        }
    }
}