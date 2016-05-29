using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Logios.DTOs
{
    public class TopicDTO
    {
        [Required(ErrorMessage = "&diams; Debe elegir una temática.")]
        public int TopicId { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir una descripción.")]
        public string Description { get; set; }
    }
}