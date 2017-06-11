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

        [HttpPost]
        public ActionResult SignUpStudent(SignUpViewModel sendedModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SignUpForm");
            }

            Account result = ElearnerDataLayoutActions.SignUp(sendedModel.UserPersonalInfo.Name, sendedModel.UserPersonalInfo.Lastname,
                        sendedModel.UserPersonalInfo.Birthdate, sendedModel.UserAccount.Email,
                        sendedModel.UserAccount.Password, sendedModel.UserBankAccount.Deposit);
            if (result == null)
            {
                return View("SignUpForm");
            }

            Session[UserType.LoggedInUser.ToString()] = result;
            return View("SuccessfulSignUp");
        }

        public ActionResult SignUpTeacherForm ()
        {
            return View();
        }

        //TODO: Create View for success
        [HttpPost]
        public ActionResult SignUpTeacher (SignUpTeacherViewModel sendedModel)
        {
            if (!ModelState.IsValid)
            {
                return View("SignUpTeacherForm");
            }

            Account result = ElearnerDataLayoutActions.SignUpTeacher(sendedModel);

            return Content(result.ToString());
        }

        public ActionResult Login()
        {
            Session["wrongAuthedication"] = false;

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
                Session["wrongAuthedication"] = true;
                return View(account);
            }
            

            Session[UserType.LoggedInUser.ToString()] = result;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            Session[UserType.LoggedInUser.ToString()] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}