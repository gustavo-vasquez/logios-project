using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Logios.Entities
{
    public class UserTrophy
    {
        [Key, Column(Order = 0), ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Key, Column(Order = 1), ForeignKey("Trophy")]
        public int TrophyId { get; set; }
        public Trophy Trophy { get; set; }
    }
}