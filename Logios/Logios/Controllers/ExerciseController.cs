using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logios.Services;

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

        public ActionResult Show(int id)
        {
            var exerciseToShow = services.GetExercise(id);
            return View(exerciseToShow);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Show(int id, string answer)
        {
            bool result = services.CheckAnswer(id, answer);            
            ViewBag.Result = result;

            return View(services.GetExercise(id));
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}