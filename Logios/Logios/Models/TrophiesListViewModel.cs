using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class TrophiesListViewModel
    {
        public int TrophyId { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public string Image { get; set; }
        public string Reason { get; set; }
    }
}