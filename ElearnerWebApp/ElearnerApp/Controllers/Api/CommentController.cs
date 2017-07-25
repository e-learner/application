using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ElearnerApp.Utilities;
using ElearnerApp.Models;

namespace ElearnerApp.Controllers.Api
{
    public class CommentController : ApiController
    {

        [HttpGet]
        public IHttpActionResult Comment()
        {
            List<Course> list = new List<Course>
            {
                ElearnerDataLayoutActions.GetCourseFromDb(2,null),
                ElearnerDataLayoutActions.GetCourseFromDb(3,null),
                ElearnerDataLayoutActions.GetCourseFromDb(4,null),
                ElearnerDataLayoutActions.GetCourseFromDb(5,null),
                ElearnerDataLayoutActions.GetCourseFromDb(6,null)
            };


            List<Course> list2 = new List<Course>();
            list2.Add(new Course { Name = list[0].Name, Id = list[0].Id,Duration =list[0].Duration,Price =list[0].Price,TeacherId = list[0].TeacherId,Description =list[0].Description });

            return Ok(list2);
        }


        [HttpPost]
        public IHttpActionResult Comment(Course course)
        {

            int bl = 0;

            if(course.Name == ElearnerDataLayoutActions.GetCourseFromDb(3, null).Name)
            {
                bl = 1;
            }


            return Ok(course);
        }
    }
}