using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Logios.Entities;

namespace Logios.Migrations.DataGenerators
{
    public class ExerciseDataGenerator : IDataGenerator
    {
        public void GenerateData(ApplicationDbContext context)
        {
            var newExercises = new List<Exercise>()
            {
                new Exercise {
                    ExerciseId = 1,
                    Problem = "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mtext>Calcular&#x000A0;el&#x000A0;valor&#x000A0;de&#x000A0;la&#x000A0;f&#x000F3;rmula&#x000A0;</mtext><munderover><mo>&#x02211;</mo><mrow><mi>k</mi><mo>=</mo><mn>0</mn></mrow><mi>n</mi></munderover><msup><mi>x</mi><mi>k</mi></msup><mi>=</mi><mfrac><mrow><msup><mi>x</mi><mrow><mi>n</mi><mo>+</mo><mn>1</mn></mrow></msup><mo>-</mo><mn>1</mn></mrow><mrow><mi>x</mi><mo>-</mo><mn>1</mn></mrow></mfrac><mtext>.&#x000A0;N&#x000F3;tese&#x000A0;que&#x000A0;</mtext><mi>x</mi><mtext>&#x000A0;es&#x000A0;un</mtext><mspace linebreak=\"newline\"/><mtext>n&#x000FA;mero&#x000A0;cualquiera,&#x000A0;fijo,&#x000A0;positivo&#x000A0;y&#x000A0;distinto&#x000A0;de&#x000A0;1,&#x000A0;y&#x000A0;que&#x000A0;la&#x000A0;inducci&#x000F3;n&#x000A0;</mtext><mspace linebreak=\"newline\"/><mtext>hay&#x000A0;que&#x000A0;hacerla&#x000A0;sobre&#x000A0;la&#x000A0;variable&#x000A0;</mtext><mi>n</mi><mtext>.</mtext><mspace linebreak=\"newline\"/><mspace linebreak=\"newline\"/><mfrac><mn>25</mn><mn>5</mn></mfrac><mo>+</mo><mi>x</mi><mo>=</mo><mn>30</mn></math>",
                    Development = "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mfrac><mn>25</mn><mn>5</mn></mfrac><mo>+</mo><mi>x</mi><mo>=</mo><mn>30</mn><mspace linebreak=\"newline\"/><mfrac><msup><menclose notation=\"updiagonalstrike\"><mn>25</mn></menclose><mn>5</mn></msup><msub><menclose notation=\"updiagonalstrike\"><mn>5</mn></menclose><mn>1</mn></msub></mfrac><mo>+</mo><mi>x</mi><mo>=</mo><mn>30</mn><mspace linebreak=\"newline\"/><mi>x</mi><mo>=</mo><mn>30</mn><mo>-</mo><mn>5</mn><mspace linebreak=\"newline\"/><mi>x</mi><mo>=</mo><mn>25</mn></math>",
                    Solution = "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mi>x</mi><mo>=</mo><mn>25</mn></math>",
                    Description = "Descripcion del ejercicio...",
                    Topic = context.Topics.FirstOrDefault(t => t.Description == "Derivadas"),
                    User = context.Users.FirstOrDefault(u => u.Email == "administrator@example.com")                    
                },

                new Exercise {
                    ExerciseId = 2,
                    Problem = "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><msup><mi>x</mi><mn>2</mn></msup><mo>+</mo><mn>1</mn><mo>=</mo><mn>5</mn></math>",
                    Development = "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><msup><mi>x</mi><mn>2</mn></msup><mo>+</mo><mn>1</mn><mo>=</mo><mn>5</mn><mspace linebreak=\"newline\"/><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mn>5</mn><mo>-</mo><mn>1</mn><mspace linebreak=\"newline\"/><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mn>4</mn><mspace linebreak=\"newline\"/><mi>x</mi><mo>=</mo><msqrt><mn>4</mn></msqrt><mspace linebreak=\"newline\"/><mi>x</mi><mo>=</mo><mn>2</mn></math>",
                    Solution = "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mi>x</mi><mo>=</mo><mn>2</mn></math>",
                    Description = "Descripcion del ejercicio...",
                    Topic = context.Topics.FirstOrDefault(t => t.Description == "Derivadas"),
                    User = context.Users.FirstOrDefault(u => u.Email == "cosme@fulanito.com")
                },

                new Exercise {
                    ExerciseId = 3,
                    Problem = "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mroot><mn>125</mn><mn>3</mn></mroot><mo>-</mo><mn>1</mn></math>",
                    Development = "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mroot><mn>125</mn><mn>3</mn></mroot><mo>-</mo><mn>1</mn><mspace linebreak=\"newline\"/><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mroot><msup><mn>5</mn><msup><menclose notation=\"updiagonalstrike\"><mn>3</mn></menclose><mn>1</mn></msup></msup><msup><menclose notation=\"updiagonalstrike\"><mn>3</mn></menclose><mn>1</mn></msup></mroot><mo>-</mo><mn>1</mn><mspace linebreak=\"newline\"/><msup><mi>x</mi><mn>2</mn></msup><mo>=</mo><mn>5</mn><mo>-</mo><mn>1</mn><mspace linebreak=\"newline\"/><mi>x</mi><mo>=</mo><msqrt><mn>4</mn></msqrt><mspace linebreak=\"newline\"/><mi>x</mi><mo>=</mo><mn>2</mn></math>",
                    Solution = "<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><mi>x</mi><mo>=</mo><mn>2</mn></math>",
                    Description = "Descripcion del ejercicio...",
                    Topic = context.Topics.FirstOrDefault(t => t.Description == "Polinomios"),
                    User = context.Users.FirstOrDefault(u => u.Email == "zapallo@zapallo.com")
                }
            };

            newExercises.ForEach(e => context.Exercises.Add(e));
            context.SaveChanges();
        }
    }
}