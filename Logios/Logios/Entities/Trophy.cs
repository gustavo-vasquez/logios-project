using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Logios.Entities
{
    public class Trophy
    {
        public int TrophyId { get; set; }

        public string Description { get; set; }
        public int Points { get; set; }
        public string Image { get; set; }

        public List<UserTrophy> UserTrophy { get; set; }
    }
}