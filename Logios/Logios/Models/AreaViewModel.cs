using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class AreaViewModel
    {
        [Required(ErrorMessage = "&diams; Debe elegir un área.")]
        public int TopicAreaId { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir una descripción.")]
        public string Description { get; set; }
    }
}