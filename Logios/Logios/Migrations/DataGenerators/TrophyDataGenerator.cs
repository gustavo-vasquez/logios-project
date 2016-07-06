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
                new Trophy
                {
                    TrophyId = 1,
                    Description = "Erudito Novato",
                    Points = 5,
                    Image = "/Content/images/trophys/laurel_bronze.svg",
                    Reason = "Resolver tu primer ejercicio sin ver el desarrollo"
                },
                new Trophy
                {
                    TrophyId = 2,
                    Description = "Erudito Avanzado",
                    Points = 10,
                    Image = "/Content/images/trophys/laurel_plata.svg",
                    Reason = "Resolver cinco ejercicios sin ver el desarrollo"
                },
                new Trophy
                {
                    TrophyId = 3,
                    Description = "Erudito Experto",
                    Points = 15,
                    Image = "/Content/images/trophys/laurel_oro.svg",
                    Reason = "Resolver diez ejercicios sin ver el desarrollo"
                }
            };

            trophies.ForEach(t => context.Trophies.Add(t));
            context.SaveChanges();
        }
    }
}