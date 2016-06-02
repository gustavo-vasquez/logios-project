using Logios.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class TopicViewModel
    {
        public TopicDTO Topic { get; set; }
        public TopicAreaDTO TopicArea { get; set; }
    }
}