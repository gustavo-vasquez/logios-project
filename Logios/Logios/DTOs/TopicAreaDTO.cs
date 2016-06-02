using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.DTOs
{
    public class TopicAreaDTO
    {
        [Required(ErrorMessage = "&diams; Debe elegir un área.")]
        public int TopicAreaId { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir una descripción.")]
        public string Description { get; set; }

        public IEnumerable<SelectListItem> ComboTopicAreas { get; set; }
    }
}