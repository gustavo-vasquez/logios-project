using Logios.Entities;
using Logios.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.Controllers
{
    public class HomeController : Controller
    {
        ExerciseServices services = new ExerciseServices();

        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public ActionResult Search()
        {
            var id = int.Parse(Request.Form["topicId"]);
            var exercises = services.GetExercisesByTopic(id);
            return View(exercises);
        }
    }
}