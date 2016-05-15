using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class TopicAreaViewModel
    {
        public Dictionary<string, IEnumerable<Topic>> TopicsByArea { get; set; }

        public TopicAreaViewModel()
        {
            this.TopicsByArea = new Dictionary<string, IEnumerable<Topic>>();
        }
    }
}