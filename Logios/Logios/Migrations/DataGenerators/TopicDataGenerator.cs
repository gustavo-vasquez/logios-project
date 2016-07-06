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
                new Topic { TopicId = 3, Description = "Tópico de Prueba A-A" },
                new Topic { TopicId = 4, Description = "Tópico de Prueba A-B" },
                new Topic { TopicId = 5, Description = "Tópico de Prueba B-A" },
                new Topic { TopicId = 6, Description = "Tópico de Prueba B-B" },
            };

            newTopics.ForEach(t => context.Topics.Add(t));
            context.SaveChanges();
        }
    }
}