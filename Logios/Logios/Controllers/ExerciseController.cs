using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using Logios.Services;
using System.Web.Script.Serialization;
using Logios.Entities;

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
            bool result = services.CheckAnswer(id, answer);            
            ViewBag.Result = result;

            return View(services.GetExerciseInformation(id));
        }

        [HttpPost]
        public ActionResult ShowDevelop(UserExercise model)
        {
            if(model != null)
            { 
                if (!services.CheckUserAlreadyResolved(model.UserId,model.ExerciseId))
                {
                    services.UpdateUserExercise(model.UserId, model.ExerciseId);
                }
                return Json("Acabas de visualizar el resultado. Ya no puedes ganar puntos por este ejercicio");
            }
            else
            {
                return Json("Problema al grabar en la BD");
            }
             
        }        
        
        public JsonResult Pagination(int id)
        {
            return Json(JsonConvert.SerializeObject(services.GetExerciseInformation(id)), JsonRequestBehavior.AllowGet);
        }

    }
}