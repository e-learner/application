using ElearnerApp.Models;
using ElearnerApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElearnerApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index ()
        {
            return View();
        }

        public ActionResult About ()
        {
            ViewBag.Message = "Codenerds Corporation";

            return View();
        }

        public ActionResult Contact ()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Teachers()
        {
            var teacherList = ElearnerDataLayoutActions.GetTeachers();

            return View(teacherList);
        }

        public ActionResult Subscriptions(int id)
        {
            IList<Subscription> subscriptions = ElearnerDataLayoutActions.GetStudentSubscriptions(id);

            return View(subscriptions);
        }


        // Only for testing
        public ActionResult Test()
        {
            var vm = ElearnerDataLayoutActions.GetMostPopular();
            return View(vm);
        }
    }
}