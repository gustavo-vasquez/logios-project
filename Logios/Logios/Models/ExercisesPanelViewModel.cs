using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class ExercisesPanelViewModel
    {
        public int ExerciseId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string TopicName { get; set; }
        public string UserName { get; set; }
    }
}