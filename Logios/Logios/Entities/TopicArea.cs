using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Entities
{
    public class TopicArea
    {
        public int TopicAreaId { get; set; }
        public string Description { get; set; }

        public List<TopicAreaTopic> TopicAreaTopic { get; set; }
    }
}