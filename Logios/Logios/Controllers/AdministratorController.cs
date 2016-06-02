using Logios.DTOs;
using Logios.Models;
using Logios.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Logios.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministratorController : Controller
    {
        AdministratorServices adminServices = new AdministratorServices();

        // GET: Administrator
        public ActionResult ControlPanel()
        {
            return View();
        }
        
        public ActionResult Exercises()
        {
            return PartialView("_Exercises", adminServices.GetAllExercises());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchExercises(string topicDescription)
        {
            ViewData["searchBox"] = topicDescription;
            return PartialView("_Exercises", adminServices.SearchExercisesByTopic(topicDescription));
        }

        public ActionResult AddExercise()
        {
            var model = new CreateExerciseViewModel();            
            model.ComboTopics = adminServices.GetAllTopics();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult AddExercise(CreateExerciseViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = adminServices.CreateNewExercise(model, User.Identity.GetUserId());
                    return PartialView("_CreateExerciseResult", result);
                }

                throw new Exception();
            }            
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }            
        }

        public ActionResult EditExercise(int id)
        {                        
            return View(adminServices.GetExerciseDataCreated(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditExercise(int id, EditExerciseViewModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = adminServices.EditCurrentExercise(id, model, User.Identity.GetUserId());
                    return PartialView("_EditExerciseResult", result);                    
                }

                throw new Exception();
            }
            catch
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult DeleteExercise(int? id, int? test)
        {
            return PartialView("_DeleteForm", id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteExercise(int? id)
        {
            TempData["result"] = adminServices.DeleteExerciseFromDB(id);

            return RedirectToAction("ControlPanel", "Administrator");
        }

        public ActionResult Topics()
        {
            return PartialView("_Topics", adminServices.GetTopicsCreated());
        }

        public ActionResult CreateTopic()
        {
            var model = new TopicViewModel { TopicArea = new TopicAreaDTO() };
            model.TopicArea.ComboTopicAreas = adminServices.GetTopicAreas();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateTopic(TopicViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(!adminServices.TopicExist(model.Topic.Description))
                    {
                        adminServices.CreateNewTopic(model.Topic.Description, model.TopicArea.TopicAreaId);
                        return RedirectToAction("ControlPanel", "Administrator");
                    }
                    else
                    {
                        ModelState.AddModelError("Topic.Description", "- La temática escrita ya existe.");
                        model.TopicArea.ComboTopicAreas = adminServices.GetTopicAreas();
                        return View(model);
                    }                    
                }

                throw new Exception();
            }
            catch
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        public ActionResult EditTopic(int id)
        {            
            return View(adminServices.GetTopicModelById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTopic(TopicViewModel model)
        {
            //try
            //{
                if(ModelState.IsValid)
                {
                    if(!adminServices.TopicExist(model.Topic.Description, model.Topic.TopicId))
                    {
                        adminServices.EditThisTopic(model);
                        return RedirectToAction("ControlPanel", "Administrator");
                    }
                    else
                    {
                        ModelState.AddModelError("Topic.Description", "- La temática escrita ya existe.");
                        return View(model);
                    }
                }

                return View(model);
            //}            
            //catch
            //{
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            //}            
        }

        public ActionResult DeleteTopic(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var topic = adminServices.GetTopicById(id);

            if(topic == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            return Json(topic, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ActionName("DeleteTopic")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTopicConfirmed(int? id)
        {
            try
            {
                if(id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if (!adminServices.CanDeleteTopic(Convert.ToInt32(id)))
                {
                    return Json(new { Message = "No se puede eliminar esta temática porque tiene al menos un ejercicio asignado." });
                }

                adminServices.DeleteTopic(id);
                return Json(new { Url = "/Administrator/ControlPanel" });
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}