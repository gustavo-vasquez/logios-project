using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class ExerciseResultViewModel
    {
        public IEnumerable<Exercise> Exercises { get; set; }
        public string TopicImageUrl { get; set; }
    }
}