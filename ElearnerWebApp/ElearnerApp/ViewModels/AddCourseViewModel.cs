using System.Web;
using ElearnerApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ElearnerApp.ViewModels
{
    public class AddCourseViewModel
    {
        public Course TeachingCourse { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public Question FirstQuestion { get; set; }
        public Question SecondQuestion { get; set; }
        public Question ThirdQuestion { get; set; }
        public Question ForthQuestion { get; set; }
        public Question FifthQuestion { get; set; }

        //TODO: Better Way!!
        public void AddQuestions ()
        {
            TeachingCourse.Questions.Add(FirstQuestion);
            TeachingCourse.Questions.Add(SecondQuestion);
            TeachingCourse.Questions.Add(ThirdQuestion);
            TeachingCourse.Questions.Add(ForthQuestion);
            TeachingCourse.Questions.Add(FifthQuestion);
        }
    }
}