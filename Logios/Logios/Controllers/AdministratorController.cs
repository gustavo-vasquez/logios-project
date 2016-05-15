using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult ControlPanel()
        {
            return View();
        }

        public ActionResult AddExercise()
        {
            return View();
        }
    }
}