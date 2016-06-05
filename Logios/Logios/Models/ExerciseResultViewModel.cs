using Logios.DTOs;
using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class ExerciseResultViewModel
    {
        public IEnumerable<ExerciseDTO> Exercises { get; set; }
        public string TopicImageUrl { get; set; }

        public ExerciseResultViewModel()
        {
            this.Exercises = new List<ExerciseDTO>();
            this.TopicImageUrl = string.Empty;
        }
    }
}