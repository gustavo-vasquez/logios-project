using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.DTOs
{
    public class FavoriteExerciseDTO
    {
        public int ExerciseId { get; set; }
        public string Description { get; set; }
        public bool Resolved { get; set; }
    }
}