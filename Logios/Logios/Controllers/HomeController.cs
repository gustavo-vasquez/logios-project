using Logios.DTOs;
using Logios.Entities;
using Logios.Models;
using Logios.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.Controllers
{
    public class HomeController : Controller
    {
        private ExerciseServices ExerciseService = new ExerciseServices();
        private TopicsService TopicService = new TopicsService();

        public ActionResult Index()
        {            
            return View();
        }

        [HttpPost]
        public PartialViewResult Search(string topicId)
        {
            var resultsViewModel = new ExerciseResultViewModel();

            if (!string.IsNullOrEmpty(topicId))
            {
                var id = int.Parse(topicId);

                var topic = this.TopicService.GetById(id);
                var exercises = ExerciseService.GetExercisesByTopic(id);
                var userId = User.Identity.GetUserId();

                resultsViewModel.Exercises = ExerciseService.GetExerciseDTOsByTopic(userId, id);
                resultsViewModel.TopicImageUrl = string.Concat(@"/Content/images/thumbnails/", topic.Description, ".png");
            }            

            return PartialView("_ExerciseSearchResult", resultsViewModel);
        }

        public JsonResult GetTopics()
        {
            var topics = this.TopicService.GetAll();
            return Json(topics, JsonRequestBehavior.AllowGet);
        }
    }
}