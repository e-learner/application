﻿using System;
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

            if (Session[UserType.LoggedInUser.ToString()] == null)
            {
                ViewData["LogInFirst"] = true;
                return RedirectToAction("LogIn", "Authedication");
            }

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
                    Questions = questions,
                    
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