using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class ExercisesByAreaViewModel
    {
        public IEnumerable<Topic> Topics { get; set; }
        public IEnumerable<ExerciseResultViewModel> ExerciseResultViewModels { get; set; }

        public ExercisesByAreaViewModel()
        {
            this.Topics = new List<Topic>();
            this.ExerciseResultViewModels = new List<ExerciseResultViewModel>();
        }
    }
}