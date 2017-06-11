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

        public ActionResult EditCourse (int id)
        {
            if (Session[UserType.LoggedInUser.ToString()] != null && ((Account)Session[UserType.LoggedInUser.ToString()]).Teacher != null)
            {
                Course teachersCourse = ElearnerDataLayoutActions.GetFullCourseDetails(id);

                return View(teachersCourse);
            }
            else
                return RedirectToAction("Index", "Home");
            
        }

        [HttpPost]
        public ActionResult SaveCourseChanges(Course course)
        {
            ElearnerDataLayoutActions.UpdateCourse(course);

            return RedirectToAction("CourseView","Courses",new {id =course.Id });
        }

        public ActionResult Purchase(Course current)
        {
            ViewData["LogInFirst"] = false;

            if (Session[UserType.LoggedInUser.ToString()] == null)
            {
                ViewData["LogInFirst"] = true;
                return RedirectToAction("LogIn", "Authedication");
            }

            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            Account logInUser = (Account)Session[UserType.LoggedInUser.ToString()];

            if (logInUser == null)
            {
                purchaseViewModel.ResultMessase = "You must Sign Up First!";
                return View(purchaseViewModel);
            }

            purchaseViewModel.ResultMessase = ElearnerDataLayoutActions.PurchaseCourse(current.Id, logInUser.Id, current.Price);
            purchaseViewModel.SelectedCourse = current;
            logInUser.BankAccount.Deposit = ElearnerDataLayoutActions.UpdateUserDeposit(logInUser.Id);

            return View(purchaseViewModel);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Content(int id)
        {
            Course result = ElearnerDataLayoutActions.GetContent(id);
            Session["LogInFirst"] = false;

            if (Session[UserType.LoggedInUser.ToString()] == null && result.Price > 0)
            {
                Session["LogInFirst"] = true;
                return RedirectToAction("LogIn", "Authedication");
            }

            return View(result);
        }

        public ActionResult Quiz (int Id)
        {
            if (Session[UserType.LoggedInUser.ToString()] == null)
            {
                return RedirectToAction("LogIn", "Authedication");
            }
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                var course = dbContext.Courses.Where(c => c.Id == Id).First();
                var questions = dbContext.Questions.Where(q => q.CourseId == course.Id).ToList();
            
                var viewModel = new QuizViewModel
                {
                    Course = course,
                    Questions = questions,
                    
                };
            
                return View(viewModel);
            }

        }

        [HttpPost]
        public ActionResult QuizResults (QuizViewModel vm)
        {
            QuizViewModel oldviewModel = (QuizViewModel)TempData["FullModel"];
            QuizViewModel newViewModel = oldviewModel;
            newViewModel.QuizResults = new List<bool>();
            Account logInUser = (Account)Session[UserType.LoggedInUser.ToString()];

            for (int i = 0; i < oldviewModel.Questions.Count; i++)
            {
                newViewModel.QuizResults.Add((oldviewModel.Questions[i].Answer == vm.UserAnswers[i]) ? true : false);
            }

            byte? grade = (byte)newViewModel.QuizResults.Where(x => x == true).Count();
            ElearnerDataLayoutActions.UpdateGradeToDb(grade, logInUser.Id, oldviewModel.Course.Id);

            return View(newViewModel);
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