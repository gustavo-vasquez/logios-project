using Logios.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.Controllers
{
    public class TopicAreaController : Controller
    {
        private TopicAreaService TopicAreaService;

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
    }
}