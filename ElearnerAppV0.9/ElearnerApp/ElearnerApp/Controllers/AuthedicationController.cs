using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElearnerApp.ViewModels;
using ElearnerApp.Utilities;
using ElearnerApp.Models;

namespace ElearnerApp.Controllers
{
    public class AuthedicationController : Controller
    {
        // GET: Authedication
        public ActionResult SignUpForm()
        {
            return View();
        }

        //TODO: Add Redirect
        [HttpPost]
        public ActionResult SignUpStudent(SignUpViewModel sendedModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SignUpForm");
            }

            if (!ElearnerDataLayoutActions.SignUp(sendedModel.UserPersonalInfo.Name,sendedModel.UserPersonalInfo.Lastname,
                        sendedModel.UserPersonalInfo.Birthdate, sendedModel.UserAccount.Email,
                        sendedModel.UserAccount.Password, sendedModel.UserBankAccount.Deposit))
            {
                return View("SignUpForm");
            }
            
            return View("SuccessfulSignUp");
        }
        public ActionResult Login ()
        {
            return View();
        }

        // method for post Log In
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login (Account account)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            Account result = ElearnerDataLayoutActions.Login(account.Email, account.Password);

            if (result == null)
            {
                return View(account);
            }

            Session["LogInUser"] = result;
            //return RedirectToAction("Index", "Courses");
            return Content(((Account)Session["LogInUser"]).ToString());
        }
    }
}