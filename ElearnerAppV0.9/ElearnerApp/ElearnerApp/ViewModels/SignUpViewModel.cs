using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElearnerApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ElearnerApp.ViewModels
{
    public class SignUpViewModel
    {
        public Account UserAccount { get; set; }
        public Student UserPersonalInfo { get; set; }
        public BankAccount UserBankAccount { get; set; }

        [Required]
        //TODO: compare passwords!
        //[Compare("UserAccount.Password")]
        public string ConfirmationPassword { get; set; }

        public override string ToString ()
        {
            return $"{UserPersonalInfo.Name} {UserPersonalInfo.Lastname} {UserPersonalInfo.Birthdate} {UserAccount.Email} {UserAccount.Password} {ConfirmationPassword}";
        }
    }
}