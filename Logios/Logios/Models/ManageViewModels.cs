using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Logios.Entities;
using System.Linq;
using System;
using Logios.DTOs;

namespace Logios.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
        public string ProfilePicture { get; set; }

        [FileSize((2 * 1024 * 1024))]
        [FileTypes("jpg,jpeg,png")]
        public System.Web.HttpPostedFileBase newAvatar { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]        
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,50}$", ErrorMessage = "La contraseña debe tener al menos 1 minúscula, 1 mayúscula y un 1 número (Mínimo 6 caracteres).")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("NewPassword", ErrorMessage = "El campo Contraseña y Confirmar contraseña no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "El campo Contraseña actual es obligatorio.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "El campo Nueva contraseña es obligatorio.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{6,50}$", ErrorMessage = "La contraseña debe tener al menos 1 minúscula, 1 mayúscula y un 1 número (Mínimo 6 caracteres).")]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "El campo Nueva contraseña y Confirmar no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
    public class ChangeUserNameViewModel
    {       
        [Display(Name = "Usuario Actual")]
        public string OldName { get; set; }

        [Required(ErrorMessage = "El campo Nuevo nombre es obligatorio.")]
        [RegularExpression(@"^([A-Za-zñ]{1})+([\w]{3,})$", ErrorMessage = "El Nombre de usuario sólo puede contener mayúsculas, minúsculas, números o un guiónbajo (sin espacios y empezar con una letra). Mínimo 4 caracteres.")]
        [Display(Name = "Nuevo nombre de usuario")]
        public string NewName { get; set; }

    }
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Número de teléfono")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Número de teléfono")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

    public class BadgesAndPointsUserPanelViewModel
    {
        public int Points { get; set; }
        public IEnumerable<UserBadgesDTO> Badges { get; set; }
    }

    public class ExerciseUserPanelViewModel
    {
        public Dictionary<string, Dictionary<string,int>> Statistics { get; set; }

        public ExerciseUserPanelViewModel()
        {
            this.Statistics = new Dictionary<string, Dictionary<string, int>>();
        }

        public void SetExercisesResolvedByTopic(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var topicAreas = context.TopicAreas.ToList().OrderBy(x => x.Description);
                var resolvedExercises = context.UserExercise
                                                    .Where(x => x.UserId == userId && x.ShowedSolution == false)
                                                    .Select(x => x.Exercise);

                foreach (var topicArea in topicAreas)
                {
                    var resolvedExercisesByArea = new Dictionary<string, int>();

                    var topicsInArea = context.TopicAreaTopics
                                                    .Where(x => x.TopicAreaId == topicArea.TopicAreaId)
                                                    .Select(x => x.Topic)
                                                    .ToList();

                    foreach (var topic in topicsInArea)
                    {
                        var resolvedCount = resolvedExercises
                                                .Count(x => x.Topic.TopicId == topic.TopicId);

                        resolvedExercisesByArea.Add(topic.Description, resolvedCount);
                    }

                    this.Statistics.Add(topicArea.Description, resolvedExercisesByArea);
                }
            }
        }
    }

    public class FavoriteExercisesPanelViewModel
    {
        public List<FavoriteExerciseDTO> FavoriteExercises;

        public FavoriteExercisesPanelViewModel()
        {
            this.FavoriteExercises = new List<FavoriteExerciseDTO>();
        }

        public void LoadFavoriteExercises(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var favoriteExercises = context.UserFavorites.Where(x => x.UserId == userId).ToList();
                var userExercises = context.UserExercise.Where(x => x.UserId == userId).ToList();

                foreach (var favoriteExercise in favoriteExercises)
                {
                    var newFavoriteExerciseDTO = new FavoriteExerciseDTO();
                    var exercise = context.Exercises.First(x => x.ExerciseId == favoriteExercise.ExerciseId);

                    newFavoriteExerciseDTO.ExerciseId = exercise.ExerciseId;
                    newFavoriteExerciseDTO.Description = exercise.Description;

                    var resolved = userExercises.Any(x => x.ExerciseId == exercise.ExerciseId
                                                       && x.ShowedSolution == false);

                    newFavoriteExerciseDTO.Resolved = resolved;

                    this.FavoriteExercises.Add(newFavoriteExerciseDTO);
                }
            }
        }
    }
}