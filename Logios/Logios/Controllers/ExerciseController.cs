using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Mvc;
using Logios.Services;
using System.Web.Script.Serialization;

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

        public void ShowAnswer(string UserId, int id)
        {
            if (!services.CheckUserAlreadyResolved(UserId,id))
            {
                services.UpdateUserExercise(UserId, id);
            }
        }        
        
        public JsonResult Pagination(int id)
        {
            return Json(JsonConvert.SerializeObject(services.GetExerciseInformation(id)), JsonRequestBehavior.AllowGet);
        }

    }
}