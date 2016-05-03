using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Entities
{
    public class UserExercise
    {
        [Key, Column(Order = 0), ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Key, Column(Order = 1), ForeignKey("Exercise")]
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }

        public bool ShowedSolution { get; set; }
        public DateTime SolvedDate { get; set; }
    }
}