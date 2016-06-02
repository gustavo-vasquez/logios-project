using Logios.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.Models
{
    public class TopicPanelViewModel
    {
        public int TopicId { get; set; }        
        public string Description { get; set; }        
        public string Area { set; get; }        
    }
}