using ElearnerApp.Models;
using ElearnerApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElearnerApp.ViewModels;
using System.IO;

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

        public ActionResult TeacherProfile(int id)
        {
            if (Session[UserType.LoggedInUser.ToString()] == null)
            {
                return RedirectToAction("Index");
            }

            IList<Course> teacherCourses = ElearnerDataLayoutActions.GetCourseByTeacher(id);

            return View(teacherCourses);
        }

        public ActionResult Subscriptions(int id)
        {
            if (Session[UserType.LoggedInUser.ToString()] == null)
            {
                return RedirectToAction("Index");
            }
            IList<Subscription> subscriptions = ElearnerDataLayoutActions.GetStudentSubscriptions(id);

            return View(subscriptions);
        }

        public ActionResult AddMoney()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMoneyToStudent(AddMoneyViewModel addMoneyViewModel)
        {
            if (!ModelState.IsValid)
                return View("AddMoney");

            Account currentUser = (Account)Session[UserType.LoggedInUser.ToString()];

            currentUser.BankAccount.Deposit = ElearnerDataLayoutActions.AddMoneyToUser(currentUser.Id, addMoneyViewModel.Amount);

            return View("Index");
        }

        public ActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourseToDb(AddCourseViewModel addCourseViewModel)
        {
            if (!ModelState.IsValid)
                return View("AddCourse");

            Account currentUser = (Account)Session[UserType.LoggedInUser.ToString()];
            addCourseViewModel.AddQuestions();
            ElearnerDataLayoutActions.AddCourseToDb(currentUser, addCourseViewModel);

            if (addCourseViewModel.Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(addCourseViewModel.Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), Utilities.FileTools.RemoveSpacesFromFilename(addCourseViewModel.TeachingCourse.Name) + ".png");
                addCourseViewModel.Image.SaveAs(path);
            }

            return View("Index");
        }
        // Only for testing
        public ActionResult Test()
        {
            ViewBag.Kati = "case-sensitive.tydd";

            return View();
        }
        // Only for testing
        [HttpPost]
        public ActionResult TestForm (TestViewModel testViewModel)
        {
            if (testViewModel != null && testViewModel.Image.ContentLength > 0)
            {
                var fileName = Path.GetFileName(testViewModel.Image.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images"), "imgB.png");
                testViewModel.Image.SaveAs(path);
                return Content(testViewModel.Image.FileName+" ** " +fileName + " ** " + path);
            }
            else
                return Content("no");

        }

        
    }
}