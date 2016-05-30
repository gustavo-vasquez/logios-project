using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Logios.DTOs
{
    public class UserBadgesDTO
    {
        public int IdTrophy { get; set; }
        public string DescriptionTrophy { get; set; }
        public string ImageTrophy { get; set; }
        public int TrophyPoints { get; set; }
    }
}