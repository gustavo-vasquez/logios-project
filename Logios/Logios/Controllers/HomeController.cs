using Logios.Entities;
using Logios.Models;
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
        ExerciseServices ExerciseService = new ExerciseServices();
        TopicsService TopicService = new TopicsService();

        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public PartialViewResult Search(string topicId)
        {
            var id = int.Parse(topicId);
            var exercises = ExerciseService.GetExercisesByTopic(id);
            var topic = this.TopicService.GetById(id);
            var resultsViewModel = new ExerciseResultViewModel()
            {
                Exercises = exercises,
                TopicImageUrl = string.Concat(@"/Content/images/thumbnails/", topic.Description, ".png")
            };

            return PartialView("_ExerciseSearchResult", resultsViewModel);
        }
    }
}