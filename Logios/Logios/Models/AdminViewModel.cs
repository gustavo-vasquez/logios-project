using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo E-mail es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ha ingresado un correo electrónico inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre de Usuario es obligatorio.")]
        [RegularExpression(@"^([A-Za-zñ]{1})+([\w]{3,})$", ErrorMessage = "El Nombre de usuario sólo puede contener mayúsculas, minúsculas, números o un guiónbajo (sin espacios y empezar con una letra). Mínimo 4 caracteres.")]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}