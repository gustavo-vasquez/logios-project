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

            bool result = services.CheckAnswer(id, answer);

            if (result)
            {
                if (!services.CheckUserHasRecord(currentUser, id))
                {
                    services.UpdateUserExercise(currentUser, id, false);
                    services.SumPoints(currentUser);
                }
            }

            ViewBag.Result = result;

            return View(services.GetExerciseInformation(id));
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
            return Json("Acabas de visualizar el resultado. Ya no puedes ganar puntos por este ejercicio");

        }        
        
        public JsonResult Pagination(int id)
        {
            return Json(JsonConvert.SerializeObject(services.GetExerciseInformation(id)), JsonRequestBehavior.AllowGet);
        }

    }
}