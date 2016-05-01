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
        ExerciseServices Services = new ExerciseServices();

        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public PartialViewResult Search(string topicId)
        {
            var id = int.Parse(topicId);
            var exercises = Services.GetExercisesByTopic(id);
            return PartialView("_ExerciseSearchResult", exercises);
        }
    }
}