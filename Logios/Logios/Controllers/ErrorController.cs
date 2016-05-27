using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Logios.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult NoScript()
        {
            return View();
        }
    }
}