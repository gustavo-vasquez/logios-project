using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Logios.DTOs
{
    public class ExerciseDTO
    {
        public int ExerciseId { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir el enunciado.")]        
        public string Problem { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir el desarrollo.")]        
        public string Development { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir una solución.")]        
        public string Solution { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir una descripción.")]        
        public string Description { get; set; }

        public bool Resolved { get; set; }
    }
}