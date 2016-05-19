using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Logios.DTOs
{
    public class ExerciseDTO
    {
        [Required(ErrorMessage = "&diams; Debe escribir el enunciado.")]
        //[RegularExpression("^((?!\\b<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>\\b).)*$", ErrorMessage = "&diams; Debe escribir un problema a resolver.")]
        public string Problem { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir el desarrollo.")]
        //[RegularExpression("^((?!\b<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>\b).)*$", ErrorMessage = "&diams; Debe escribir el desarrollo.")]
        public string Development { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir una solución.")]
        //[RegularExpression("^((?!\\b<math xmlns=\"http://www.w3.org/1998/Math/MathML\"/>\\b).)*$", ErrorMessage = "&diams; Debe escribir la solución.")]
        public string Solution { get; set; }

        [Required(ErrorMessage = "&diams; Debe escribir una descripción.")]
        //[RegularExpression("^((?!\\bHola\\b).)*$", ErrorMessage = "&diams; No puede escribir Hola.")]
        public string Description { get; set; }        
    }
}