using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class ExerciseViewModel
    {
        public Exercise Exercise { get; set; }
        public string topicName { get; set; }
        public bool isFirst { get; set; }
        public bool isLast { get; set; }
        public int? backExerciseId { get; set; }
        public int? nextExerciseId { get; set; }
    }
}