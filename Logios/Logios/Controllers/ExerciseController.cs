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

namespace Logios.Controllers
{
    public class ExerciseController : Controller
    {
        static ExerciseServices services = new ExerciseServices();

        // GET: Exercise
        public ActionResult Create()
        {
            return View();
        }
        
        public ActionResult Show(int? id)
        {
            if (id != null)
            {                           
                var exerciseToShow = services.GetExerciseInformation(id);
                return View(exerciseToShow);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Show(int id, string answer)
        {
            var currentUser = User.Identity.GetUserId();

            bool? result = services.CheckAnswer(id, answer);

            if (result.Value)
            {
                if (!services.CheckUserHasRecord(currentUser, id))
                {
                    services.UpdateUserExercise(currentUser, id, false);
                    services.SumPoints(currentUser);
                }
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

            if (services.CheckUserHasRecord(model.UserId,model.ExerciseId))
            {
                return Json("Ya habías resuelto este ejercicio");
            }

            services.UpdateUserExercise(model.UserId, model.ExerciseId, true);
            return Json("Acabas de visualizar la resolución. Ya no puedes ganar puntos por este ejercicio");

        }
        
        public ActionResult Report(int id)
        {
            return View(services.GetReportData(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Report(ReportViewModel model, int exerciseId, string uploadName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    services.SendReport(model.Cause, exerciseId, uploadName, User.Identity.GetUserId());

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
        
        public JsonResult Pagination(int id)
        {
            return Json(JsonConvert.SerializeObject(services.GetExerciseInformation(id)), JsonRequestBehavior.AllowGet);
        }

    }
}