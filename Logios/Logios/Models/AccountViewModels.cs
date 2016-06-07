using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Logios.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Debes escribir un nombre")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Debes escribir un mail valido")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required(ErrorMessage = "Proveedor requerido.")]
        public string Provider { get; set; }

        [Required(ErrorMessage = "Código requerido.")]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "¿Recordar este navegador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(ErrorMessage = "El campo E-mail es obligatorio.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo Nombre de Usuario es obligatorio.")]        
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "El campo E-mail es obligatorio.")]
        //[Display(Name = "E-mail")]
        //[EmailAddress(ErrorMessage = "Ha ingresado un correo electrónico inválido.")]
        //public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordarme?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El campo Nombre de Usuario es obligatorio.")]
        [RegularExpression(@"^([A-Za-zñ]{1})+([\w]{3,})$", ErrorMessage = "El Nombre de usuario sólo puede contener mayúsculas, minúsculas, números o un guiónbajo (sin espacios y empezar con una letra). Mínimo 4 caracteres.")]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo E-mail es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ha ingresado un correo electrónico inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!$%^&*()_+=\-`{}:@~#';<>?/.,|])(?=.*[0-9]).{6,50}$", ErrorMessage = "La contraseña debe tener al menos 1 mayúscula, 1 minúscula, 1 número y 1 caracter especial (Mínimo 6 caracteres).")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        [FileSize((2 * 1024 * 1024))]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase ImageProfile { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "El campo E-mail es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ha ingresado un correo electrónico inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.", MinimumLength = 6)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[!$%^&*()_+=\-`{}:@~#';<>?/.,|])(?=.*[0-9]).{6,50}$", ErrorMessage = "La contraseña debe tener al menos 1 mayúscula, 1 minúscula, 1 número y 1 caracter especial (Mínimo 6 caracteres).")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "El campo E-mail es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ha ingresado un correo electrónico inválido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxSize;

        public FileSizeAttribute(int maxSize)
        {
            _maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            return (value as HttpPostedFileBase).ContentLength <= _maxSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("*Tamaño máximo de imágen = 2 MB");
        }
    }

    public class FileTypesAttribute : ValidationAttribute
    {
        private readonly List<string> _types;

        public FileTypesAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true;

            var fileExt = System.IO.Path.GetExtension((value as HttpPostedFileBase).FileName).Substring(1);
            return _types.Contains(fileExt, System.StringComparer.OrdinalIgnoreCase);
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format("*Foto inválida. Tipos de imágen soportadas: {0}.", System.String.Join(", ", _types));
        }
    }
}
