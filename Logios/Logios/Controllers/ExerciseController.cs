using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using Logios.Services;
using System.Web.Script.Serialization;
using Logios.Entities;
using Microsoft.AspNet.Identity;
using Logios.Models;
using System.Net;

namespace Logios.Controllers
{
    public class ExerciseController : Controller
    {
        static ExerciseServices ExerciseService = new ExerciseServices();
        private TrophyService TrophyService = new TrophyService();
        private static Trophy NewTrophy;

        // GET: Exercise
        public ActionResult Create()
        {
            return View();
        }
        
        public ActionResult Show(int? id)
        {
            if (id != null)
            {
                var userId = User.Identity.GetUserId();
                var exerciseToShow = ExerciseService.GetExerciseInformation(id, userId);

                if(exerciseToShow.Exercise != null)
                {
                    using (var context = new ApplicationDbContext())
                    {
                        exerciseToShow.Favorited = context.UserFavorites.Any(x => x.ExerciseId == id
                                                                               && x.UserId == userId);
                    }

                    exerciseToShow.NewTrophy = NewTrophy;
                    NewTrophy = null;
                    return View(exerciseToShow);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }                
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Show(int id, string answer)
        {
            var currentUser = User.Identity.GetUserId();

            var result = ExerciseService.CheckAnswer(id, currentUser, answer);

            if (result.Success)
            {
                if (!ExerciseService.CheckUserHasRecord(currentUser, id))
                {
                    ExerciseService.UpdateUserExercise(currentUser, id, false);
                    ExerciseService.SumPoints(currentUser);
                }

                NewTrophy = this.TrophyService.UpdateUserTrophies(currentUser);
            }
            
            return PartialView("_Result", result);
        }

        [HttpGet]
        public ActionResult ShowDevelop(string userId, int exerciseId)
        {
            return PartialView("_ConfirmDialog", new UserExercise { UserId = userId, ExerciseId = exerciseId });
        }

        [HttpPost]
        public ActionResult ShowDevelop(UserExercise model)
        {
            if (model == null)
            {
                return Json("Error en la carga");
            }

            if (ExerciseService.CheckUserHasRecord(model.UserId,model.ExerciseId))
            {
                return Json("Ya habías visto o resuelto este ejercicio");
            }

            ExerciseService.UpdateUserExercise(model.UserId, model.ExerciseId, true);
            return Json("Acabas de visualizar la resolución. Ya no puedes ganar puntos por este ejercicio");

        }
        
        public ActionResult Report(int id)
        {
            return View(ExerciseService.GetReportData(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report(ReportViewModel model, int exerciseId, string uploadName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ExerciseService.SendReport(model.Cause, exerciseId, uploadName, User.Identity.GetUserId());

                    return RedirectToAction("Show", "Exercise", new { id = exerciseId });
                }

                throw new Exception();
            }
            catch(Exception ex)
            {
                return Content("<script>alert('" + ex.Message + "');</script>");
                //return View(services.GetReportData(exerciseId));
            }            
            
        }
        
        

        [HttpPost]
        public void ToggleFavorite()
        {            
            var userId = User.Identity.GetUserId();
            var exerciseId = int.Parse(Request.Form["exerciseId"]);

            using (var context = new ApplicationDbContext())
            {
                var exercise = context.UserFavorites.FirstOrDefault(x => x.ExerciseId == exerciseId
                                                                      && x.UserId == userId);
                if (exercise != null)
                {
                    context.UserFavorites.Remove(exercise);
                }
                else
                {
                    context.UserFavorites.Add(new UserFavorite()
                    {
                        UserId = userId,
                        ExerciseId = exerciseId
                    });
                }

                context.SaveChanges();
            }
        }
         
        
        [HttpGet]
        public ActionResult HelperEditor()
        {
            return View();
        }
    }
}