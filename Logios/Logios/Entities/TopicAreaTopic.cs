using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Logios.Entities
{
    public class TopicAreaTopic
    {
        [Key, Column(Order = 0), ForeignKey("Topic")]
        public int TopicId { get; set; }
        public Topic Topic { get; set; }

        [Key, Column(Order = 1), ForeignKey("TopicArea")]
        public int TopicAreaId { get; set; }
        public TopicArea TopicArea { get; set; }
    }
}