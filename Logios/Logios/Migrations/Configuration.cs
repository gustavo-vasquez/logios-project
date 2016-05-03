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
                    new Exercise {
                        Problem = "<math xmlns='http://www.w3.org/1998/Math/MathML'><mfrac><mn>25</mn><mn>5</mn></mfrac><mo>+</mo><mi>x</mi><mo>=</mo><mn>30</mn></math>",
                        Development = "<math xmlns='http://www.w3.org/1998/Math/MathML'><mfrac><mn>25</mn><mn>5</mn></mfrac><mo>+</mo><mi>x</mi><mo>=</mo><mn>30</mn><mspace linebreak='newline'/><mfrac><msup><menclose notation='updiagonalstrike'><mn>25</mn></menclose><mn>5</mn></msup><msub><menclose notation='updiagonalstrike'><mn>5</mn></menclose><mn>1</mn></msub></mfrac><mo>+</mo><mi>x</mi><mo>=</mo><mn>30</mn><mspace linebreak='newline'/><mi>x</mi><mo>=</mo><mn>30</mn><mo>-</mo><mn>5</mn><mspace linebreak='newline'/><mi>x</mi><mo>=</mo><mn>25</mn></math>",
                        Solution = "<math xmlns='http://www.w3.org/1998/Math/MathML'><mi>x</mi><mo>=</mo><mn>25</mn></math>",
                        Topic = newTopics[0]
                    },

                    new Exercise {
                        Problem = "<math xmlns='http://www.w3.org/1998/Math/MathML'><msup><mi>x</mi><mn>2</mn></msup><mo>+</mo><mn>1</mn><mo>=</mo><mn>5</mn></math>",
                        Development = "<math xmlns='http://www.w3.org/1998/Math/MathML'><msup><mi>x</mi><mn>2</mn></msup><mo>+</mo><mn>1</mn><mo>=</mo><mn>5</mn><mspace linebreak='newline'/><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mn>5</mn><mo>-</mo><mn>1</mn><mspace linebreak='newline'/><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mn>4</mn><mspace linebreak='newline'/><mi>x</mi><mo>=</mo><msqrt><mn>4</mn></msqrt><mspace linebreak='newline'/><mi>x</mi><mo>=</mo><mn>2</mn></math>",
                        Solution = "<math xmlns='http://www.w3.org/1998/Math/MathML'><mi>x</mi><mo>=</mo><mn>2</mn></math>",
                        Topic = newTopics[0] },

                    new Exercise {
                        Problem = "<math xmlns='http://www.w3.org/1998/Math/MathML'><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mroot><mn>125</mn><mn>3</mn></mroot><mo>-</mo><mn>1</mn></math>",
                        Development = "<math xmlns='http://www.w3.org/1998/Math/MathML'><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mroot><mn>125</mn><mn>3</mn></mroot><mo>-</mo><mn>1</mn><mspace linebreak='newline'/><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mroot><msup><mn>5</mn><msup><menclose notation='updiagonalstrike'><mn>3</mn></menclose><mn>1</mn></msup></msup><msup><menclose notation='updiagonalstrike'><mn>3</mn></menclose><mn>1</mn></msup></mroot><mo>-</mo><mn>1</mn><mspace linebreak='newline'/><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mn>5</mn><mo>-</mo><mn>1</mn><mspace linebreak='newline'/><mi>x</mi><mo>=</mo><msqrt><mn>4</mn></msqrt><mspace linebreak='newline'/><mi>x</mi><mo>=</mo><mn>2</mn></math>",
                        Solution = "<math xmlns='http://www.w3.org/1998/Math/MathML\'><mi>x</mi><mo>=</mo><mn>2</mn></math>",
                        Topic = newTopics[1] }
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
