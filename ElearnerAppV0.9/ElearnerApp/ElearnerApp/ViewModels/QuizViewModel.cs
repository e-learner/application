using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElearnerApp.Models;

namespace ElearnerApp.ViewModels
{
    public class QuizViewModel
    {
        public Course Course { get; set; }
        public List<Question> Questions { get; set; }
        public List<bool> Answers { get; set; }
        
    }
}