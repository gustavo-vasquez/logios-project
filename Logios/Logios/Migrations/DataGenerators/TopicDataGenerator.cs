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
                new Topic { TopicId = 1, Description = "Polinomios" },
                new Topic { TopicId = 2, Description = "Matrices" },
                new Topic { TopicId = 3, Description = "Temática de prueba A1" },
                new Topic { TopicId = 4, Description = "Temática de prueba A2" },
                new Topic { TopicId = 5, Description = "Temática de prueba B1" },
                new Topic { TopicId = 6, Description = "Temática de prueba B2" },
            };

            newTopics.ForEach(t => context.Topics.Add(t));
            context.SaveChanges();
        }
    }
}