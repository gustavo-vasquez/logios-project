using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logios.Entities;

namespace Logios.Migrations.DataGenerators
{
    public class TrophyDataGenerator : IDataGenerator
    {
        public void GenerateData(ApplicationDbContext context)
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