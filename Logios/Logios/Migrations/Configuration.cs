namespace Logios.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Logios.Migrations.DataGenerators;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private readonly List<IDataGenerator> DataGenerators;

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            this.DataGenerators = new List<IDataGenerator>()
            {
                new ApplicationUserGenerator(),
                new TopicDataGenerator(),
                new ExerciseDataGenerator(),
                new TrophyDataGenerator(),
                new TopicAreaDataGenerator(),
                new TopicAreaTopicDataGenerator()                
            };
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  Borrar toda la data, en orden para que no de error de foreign keys
            context.TopicAreaTopics.Clear();
            context.Exercises.Clear();
            context.Topics.Clear();
            context.TopicAreas.Clear();
            context.Trophies.Clear();
            
            context.SaveChanges();

            // Recorrer el listado de generadores de data y llamar a su método GenerateData en orden
            this.DataGenerators.ForEach(dataGenerator => dataGenerator.GenerateData(context));
        }
    }
}
