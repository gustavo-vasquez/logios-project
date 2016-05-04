using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}