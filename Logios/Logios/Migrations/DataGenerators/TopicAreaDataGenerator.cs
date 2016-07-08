using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logios.Entities;

namespace Logios.Migrations.DataGenerators
{
    public class TopicAreaDataGenerator : IDataGenerator
    {
        public void GenerateData(ApplicationDbContext context)
        {
            var newTopicAreas = new List<TopicArea>
            {
                new TopicArea { TopicAreaId = 1, Description = "Algebra" },
                new TopicArea { TopicAreaId = 2, Description = "Área de prueba A" },
                new TopicArea { TopicAreaId = 3, Description = "Área de prueba B" },
            };

            newTopicAreas.ForEach(t => context.TopicAreas.Add(t));
            context.SaveChanges();
        }
    }
}