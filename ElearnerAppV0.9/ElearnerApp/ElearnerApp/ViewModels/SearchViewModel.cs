using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ElearnerApp.Models;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace ElearnerApp.ViewModels
{
    public class SearchViewModel
    {
        [DisplayName("Search Courses")]
        public string Query { get; set; }
        [DisplayName("Min Price")]
        public decimal? LowerBoundPrice { get; set; }
        [DisplayName("Max Price")]
        public decimal? UpperBoundPrice { get; set; }
        [DisplayName("Min Duration")]
        public int? LowerBoundDuration { get; set; }
        [DisplayName("Max Duration")]
        public int? UpperBoundDuration { get; set; }
        public List<Course> SearchResults { get; set; }
        [DisplayName("Order By")]
        public string Order { get; set; }

        public List<Course> Search ()
        {
            if (Order == null)
            {
                Order = "Name";
            }

            using (ElearnerContext dbContext = new ElearnerContext())
            {
                if (LowerBoundPrice == null && UpperBoundPrice == null &&
                    LowerBoundDuration == null && UpperBoundDuration == null && Query != null)
                {
                    if (Order == "Name")
                    {
                        var q1 = dbContext.Courses.Where(x => x.Name.Contains(Query)).OrderBy(x => x.Name);
                        return (Query.Length == 1) ? q1.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q1.ToList();
                    }
                    if (Order == "Price")
                    {
                        var q1 = dbContext.Courses.Where(x => x.Name.Contains(Query)).OrderBy(x => x.Price);
                        return (Query.Length == 1) ? q1.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q1.ToList();
                    }
                    if (Order == "Duration")
                    {
                        var q1 = dbContext.Courses.Where(x => x.Name.Contains(Query)).OrderBy(x => x.Duration);
                        return (Query.Length == 1) ? q1.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q1.ToList();
                    }
                    return null;
                }
                if (LowerBoundPrice != null && UpperBoundPrice != null &&
                    LowerBoundDuration == null && UpperBoundDuration == null && Query != null)
                {
                    if (Order == "Name")
                    {
                        var q2 = dbContext.Courses.Where(x => x.Name.Contains(Query) && x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice).OrderBy(x => x.Name);
                        return (Query.Length == 1) ? q2.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q2.ToList();
                    }
                    if (Order == "Price")
                    {
                        var q2 = dbContext.Courses.Where(x => x.Name.Contains(Query) && x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice).OrderBy(x => x.Price);
                        return (Query.Length == 1) ? q2.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q2.ToList();
                    }
                    if (Order == "Duration")
                    {
                        var q2 = dbContext.Courses.Where(x => x.Name.Contains(Query) && x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice).OrderBy(x => x.Duration);
                        return (Query.Length == 1) ? q2.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q2.ToList();
                    }
                    return null;
                }
                if (LowerBoundPrice != null && UpperBoundPrice != null &&
                    LowerBoundDuration != null && UpperBoundDuration != null && Query != null)
                {
                    if (Order == "Name")
                    {
                        var q3 = dbContext.Courses.Where(x => x.Name.Contains(Query) && x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice && x.Duration >= LowerBoundDuration && x.Duration <= UpperBoundDuration).OrderBy(x => x.Name);
                        return (Query.Length == 1) ? q3.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q3.ToList();
                    }
                    if (Order == "Price")
                    {
                        var q3 = dbContext.Courses.Where(x => x.Name.Contains(Query) && x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice && x.Duration >= LowerBoundDuration && x.Duration <= UpperBoundDuration).OrderBy(x => x.Price);
                        return (Query.Length == 1) ? q3.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q3.ToList();
                    }
                    if (Order == "Duration")
                    {
                        var q3 = dbContext.Courses.Where(x => x.Name.Contains(Query) && x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice && x.Duration >= LowerBoundDuration && x.Duration <= UpperBoundDuration).OrderBy(x => x.Duration);
                        return (Query.Length == 1) ? q3.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q3.ToList();
                    }
                    return null;
                }
                if (LowerBoundPrice == null && UpperBoundPrice == null &&
                    LowerBoundDuration != null && UpperBoundDuration != null && Query == null)
                {
                    if (Order == "Name")
                    {
                        var q4 = dbContext.Courses.Where(x => x.Name.Contains(Query) && x.Duration >= LowerBoundDuration && x.Duration <= UpperBoundDuration).OrderBy(x => x.Name);
                        return q4.ToList();
                    }
                    if (Order == "Price")
                    {
                        var q4 = dbContext.Courses.Where(x => x.Name.Contains(Query) && x.Duration >= LowerBoundDuration && x.Duration <= UpperBoundDuration).OrderBy(x => x.Price);
                        return q4.ToList();
                    }
                    if (Order == "Duration")
                    {
                        var q4 = dbContext.Courses.Where(x => x.Name.Contains(Query) && x.Duration >= LowerBoundDuration && x.Duration <= UpperBoundDuration).OrderBy(x => x.Duration);
                        return q4.ToList();
                    }
                    return null;
                }
                if (LowerBoundPrice != null && UpperBoundPrice != null &&
                    LowerBoundDuration == null && UpperBoundDuration == null && Query == null)
                {
                    if (Order == "Name")
                    {
                        var q5 = dbContext.Courses.Where(x => x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice).OrderBy(x => x.Name);
                        return q5.ToList();
                    }
                    if (Order == "Price")
                    {
                        var q5 = dbContext.Courses.Where(x => x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice).OrderBy(x => x.Price);
                        return q5.ToList();
                    }
                    if (Order == "Duration")
                    {
                        var q5 = dbContext.Courses.Where(x => x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice).OrderBy(x => x.Duration);
                        return q5.ToList();
                    }
                    return null;
                }
                if (LowerBoundPrice != null && UpperBoundPrice != null &&
                    LowerBoundDuration != null && UpperBoundDuration != null && Query == null)
                {
                    if (Order == "Name")
                    {
                        var q6 = dbContext.Courses.Where(x => x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice && x.Duration >= LowerBoundDuration && x.Duration <= UpperBoundDuration).OrderBy(x => x.Name);
                        return q6.ToList();
                    }
                    if (Order == "Price")
                    {
                        var q6 = dbContext.Courses.Where(x => x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice && x.Duration >= LowerBoundDuration && x.Duration <= UpperBoundDuration).OrderBy(x => x.Price);
                        return q6.ToList();
                    }
                    if (Order == "Duration")
                    {
                        var q6 = dbContext.Courses.Where(x => x.Price >= LowerBoundPrice && x.Price <= UpperBoundPrice && x.Duration >= LowerBoundDuration && x.Duration <= UpperBoundDuration).OrderBy(x => x.Duration);
                        return q6.ToList();
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}