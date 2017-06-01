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

            if (!ElearnerDataLayoutActions.SignUp(sendedModel.UserPersonalInfo.Name, sendedModel.UserPersonalInfo.Lastname,
                        sendedModel.UserPersonalInfo.Birthdate, sendedModel.UserAccount.Email,
                        sendedModel.UserAccount.Password, sendedModel.UserBankAccount.Deposit))
            {
                return View("SignUpForm");
            }

            return View("SuccessfulSignUp");
        }

        public ActionResult Login()
        {
            ViewData["wrongAuthedication"] = false;

            return View();
        }

        // method for post Log In
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account account)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }

            Account result = ElearnerDataLayoutActions.HasAccount(account.Email, account.Password);

            if (result == null)
            {
                ViewData["wrongAuthedication"] = true;
                return View(account);
            }

            Session[UserType.LoggedInUser.ToString()] = result;
            return RedirectToAction("LogIn", "Authentication"); 
            //return Content(((Account)Session[UserType.LoggedInUser.ToString()]).ToString());
        }

        public ActionResult LogOut()
        {
            Session[UserType.LoggedInUser.ToString()] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}