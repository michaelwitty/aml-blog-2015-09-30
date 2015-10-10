using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aml.SinglePageApplicationMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexTemplate()
        {
            return PartialView("~/Views/Partials/Home/_Index.cshtml");
        }
    }
}