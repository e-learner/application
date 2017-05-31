using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ElearnerWeb.Models;

namespace ElearnerWeb.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ActionResult Students()
        {
            Student jim = new Student() { Name = "Jim"};
            
            return View(jim);
        }   

        [Route("Students/ViewCourses/{CourseId:regex(\\d{3})}")]
        public ActionResult ViewCourses(int courseId)
        {
            return Content(courseId.ToString());
        }

        public ActionResult Index()
        {
            return Content("Student Index");
        }
    }
}