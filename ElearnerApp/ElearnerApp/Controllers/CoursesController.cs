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
            ViewData["LogInFirst"] = false;

            if (TempData["Message"] != null)
            {
                ViewBag.Message = (bool)TempData["Message"];
            }


            if (Session[UserType.LoggedInUser.ToString()] == null)
            {
                ViewData["LogInFirst"] = true;
                return RedirectToAction("LogIn", "Authedication");
            }

            Course result = ElearnerDataLayoutActions.GetContent(id);

            var contentVM = new ContentViewModel
            {
                Course = result,
                Content = result.Content,
                Subscription = new Subscription()
            };
            return View(contentVM);
        }

        [HttpPost]
        public ActionResult Content(ContentViewModel contentVM)
        {
            var comment = contentVM.Subscription.Comment;
            var rating = contentVM.Subscription.Rate;
            var courseId = contentVM.Course.Id;
            int accountId = ((Account)Session[UserType.LoggedInUser.ToString()]).Id;

            using (ElearnerContext dbContext = new ElearnerContext())
            {

                if (!String.IsNullOrEmpty(comment) && rating != null)
                {
                    // If you have composite primary key, then pass key values in the order they defined in model:
                    var subscriptionInDB = dbContext.Subscriptions.Find(courseId, accountId);

                    subscriptionInDB.Comment = comment;
                    subscriptionInDB.Rate = rating;
                    try
                    {
                        dbContext.SaveChanges();
                        TempData["Message"] = true;
                    }
                    catch (Exception)
                    {
                        TempData["Message"] = false;
                    }

                    return RedirectToAction("Content", "Courses", new { id = courseId });
                }
                else
                {
                    TempData["Message"] = false;
                    return RedirectToAction("Content", "Courses", new { id = courseId });
                }

            }

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
                    Questions = questions,
                    
                };
            
                return View(viewModel);
            }

        }
        [HttpPost]
        public ActionResult QuizResults(QuizViewModel vm)
        {
            QuizViewModel oldviewModel = (QuizViewModel)TempData["FullModel"];
            QuizViewModel newViewModel = oldviewModel;
            newViewModel.QuizResults = new List<bool>();

            for (int i = 0; i < oldviewModel.Questions.Count; i++)
            {
                newViewModel.QuizResults.Add((oldviewModel.Questions[i].Answer == vm.UserAnswers[i]) ? true : false);
            }

            byte grade = (byte)newViewModel.QuizResults.Where(x => x == true).Count();
            ElearnerDataLayoutActions.UpdateGradeToDb(grade, 6, oldviewModel.Course.Id);

            return View(newViewModel);
        }

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