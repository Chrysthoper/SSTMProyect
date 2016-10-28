using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSTM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Super Simple Task Manager";
            return View();
        }

        public ActionResult NewTask()
        {
            return View();
        }

        public ActionResult ListUsers()
        {
            return View();
        }

        public PartialViewResult TemplateTasks()
        {
            return PartialView("/Views/Shared/TemplateTasks.cshtml");
        }
    }
}
