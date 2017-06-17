using ElearnerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElearnerApp.ViewModels
{
    public class ContentViewModel
    {
        public Content Content { get; set; }
        public Course Course { get; set; }
        public Subscription Subscription { get; set; }
    }
}