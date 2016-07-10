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
    public class TopicController : Controller
    {
        private readonly ExerciseServices ExerciseService;
        private readonly TopicsService TopicService;

        public TopicController()
        {
            this.ExerciseService = new ExerciseServices();
            this.TopicService = new TopicsService();
        }

        // GET: Topic
        public ActionResult Index(int topicId)
        {
            var userId = User.Identity.GetUserId();
            var topic = this.TopicService.GetById(topicId);

            var exercises = this.ExerciseService.GetExerciseDTOsCards(userId, topicId, true);
            
            var resultsViewModel = new ExerciseResultViewModel()
            {
                Exercises = exercises,
                TopicImageUrl = string.Concat(@"/Content/images/thumbnails/", topic.Description.Replace(' ', '_'), ".png")
            };

            ViewBag.TopicDescription = topic.Description;

            return View(resultsViewModel);
        }
    }
}