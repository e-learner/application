using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElearnerApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ElearnerApp.ViewModels
{
    public class SignUpTeacherViewModel
    {
        public Account TeacherAccount { get; set; }
        public Course TeachingCourse{ get; set; }

        [Required]
        //[Compare("UserAccount.Password")]
        public string ConfirmationPassword { get; set; }

        public override string ToString ()
        {
            return $"{TeacherAccount.Teacher.Name} {TeacherAccount.Teacher.Lastname} {TeacherAccount.Email} {TeacherAccount.Password} {ConfirmationPassword}";
        }
    }
}