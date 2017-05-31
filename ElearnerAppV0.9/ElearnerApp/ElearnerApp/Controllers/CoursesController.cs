using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElearnerApp.Models;
using ElearnerApp.Utilities;
using ElearnerApp.ViewModels;

namespace ElearnerApp.Controllers
{
    public class CoursesController : Controller
    {
        // GET: Courses
        public ActionResult CourseView(int id)
        {
            Course queryResult = ElearnerDataLayoutActions.GetCourseFromDb(id, null);
            return View(queryResult);
        }

        public ActionResult Purchase(Course current)
        {
            if (AppManager.LoggedInUser == null)
                return Content("You must Sign Up First!");

            string result = ElearnerDataLayoutActions.PurchaseCourse(current.Id, AppManager.LoggedInUser, current.Price);

            return Content(result);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Content (int id)
        {
            Course result = ElearnerDataLayoutActions.GetContent(id);

            return View(result);
        }

        
        public ActionResult Quiz (int Id)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                var course = dbContext.Courses.Where(c => c.Id == Id).First();
                var questions = dbContext.Questions.Where(q => q.CourseId == course.Id).ToList();
            
                var viewModel = new QuizViewModel
                {
                    Course = course,
                    Questions = questions
                };
            
                return View(viewModel);
            }

        }
        [HttpPost]
        public ActionResult QuizResults (QuizViewModel vm)
        {
            QuizViewModel viewModel = (QuizViewModel)TempData["FullModel"];
            viewModel.Answers = vm.Answers;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SearchResult(SearchViewModel vm)
        {
            vm.SearchResults = new List<Course>();

            if (!ModelState.IsValid || vm.Search() == null)
            {
                TempData["InvalidSearchMessage"] = "Invalid Search";
                return RedirectToAction("Index");
            }

            foreach (var item in vm.Search())
            {
                vm.SearchResults.Add(ElearnerDataLayoutActions.GetCourseFromDb(item.Id, null));
            }

            return View(vm);
        }
    }
}