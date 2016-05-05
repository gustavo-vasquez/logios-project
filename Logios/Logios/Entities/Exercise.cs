using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Entities
{
    public class Exercise
    {
        public int ExerciseId { get; set; }
        public string Problem { get; set; }
        public string Development { get; set; }
        public string Solution { get; set; }
        public string Description { get; set; }

        public virtual Topic Topic { get; set; }
    }
}