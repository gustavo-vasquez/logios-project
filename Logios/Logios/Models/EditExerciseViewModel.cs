using Logios.DTOs;
using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.Models
{
    public class EditExerciseViewModel
    {
        public ExerciseDTO Exercise { get; set; }
        public Topic Topic { get; set; }
        public IEnumerable<SelectListItem> ComboTopics { get; set; }
    }
}