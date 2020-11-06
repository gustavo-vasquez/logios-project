using Logios.DTOs;
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
    public class TopicAreaController : Controller
    {
        private readonly TopicAreaService TopicAreaService;

        public TopicAreaController()
        {
            this.TopicAreaService = new TopicAreaService();
        }

        // GET: TopicArea
        public ActionResult Index()
        {
            var viewModel = this.TopicAreaService.GetAll();

            return View(viewModel);
        }

        public ActionResult ExercisesByArea(string topicAreaDescription)
        {
            var userId = User.Identity.GetUserId();
            var viewModel = this.TopicAreaService.GetExercisesByTopicArea(userId, topicAreaDescription);
            ViewBag.TopicAreaDescription = topicAreaDescription;

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult TopicAreas()
        {
            return PartialView("_TopicAreas", TopicAreaService.GetTopicAreas());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateArea()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArea(TopicAreaDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(!TopicAreaService.AreaExists(model.Description))
                    {
                        TopicAreaService.CreateNewArea(model);
                        return RedirectToAction("ControlPanel", "Administrator");
                    }                    
                    else
                    {
                        ModelState.AddModelError("Description", "- El área especificada ya existe.");
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


        [Authorize(Roles = "Admin")]
        public ActionResult EditArea(int id)
        {            
            return View(TopicAreaService.GetTopicAreaById(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArea(TopicAreaDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(!TopicAreaService.AreaExists(model.Description, model.TopicAreaId))
                    {
                        TopicAreaService.EditThisArea(model);
                        return RedirectToAction("ControlPanel", "Administrator");
                    }
                    else
                    {
                        ModelState.AddModelError("Description", "- La área temática escrita ya existe.");
                        return View(model);
                    }                    
                }                

                return View(model);
            }
            catch
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteArea(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var area = TopicAreaService.GetTopicAreaById(Convert.ToInt32(id));

            if (area == null)
            {
                return HttpNotFound();
            }

            return Json(area, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteArea")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAreaConfirmed(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                if(!TopicAreaService.CanDeleteArea(Convert.ToInt32(id)))
                {
                    return Json(new { Message = "No se puede eliminar esta área porque tiene una o más temáticas asignadas." });
                }

                TopicAreaService.DeleteArea(Convert.ToInt32(id));
                return Json(new { Url = "/Administrator/ControlPanel" });
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}