using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElearnerApp.Models;

namespace ElearnerApp.ViewModels
{
    public class CommentViewModel
    {
        public string Comment { get; set; }
        public string StudentName { get; set; }
        public Byte? Grade { get; set; }
    }
}