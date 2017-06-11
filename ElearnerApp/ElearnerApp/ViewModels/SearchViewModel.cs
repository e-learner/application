using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ElearnerApp.Models;

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


        public List<Course> Search()
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                if (LowerBoundPrice == null && UpperBoundPrice == null &&
                    LowerBoundDuration == null && UpperBoundDuration == null && Query!=null)
                {
                    var q1 = from p in dbContext.Courses
                             where p.Name.Contains(Query)
                             orderby p.Name
                             select p;

                    return (Query.Length == 1) ? q1.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q1.ToList();
                }
                if (LowerBoundPrice != null && UpperBoundPrice != null &&
                    LowerBoundDuration == null && UpperBoundDuration == null && Query != null)
                {
                    var q2 = from p in dbContext.Courses
                             where (p.Name.Contains(Query) && p.Price >= LowerBoundPrice && p.Price <= UpperBoundPrice)
                             orderby p.Name
                             select p;

                    return (Query.Length == 1) ? q2.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q2.ToList();
                }
                if (LowerBoundPrice != null && UpperBoundPrice != null &&
                    LowerBoundDuration != null && UpperBoundDuration != null && Query != null)
                {
                    var q3 = from p in dbContext.Courses
                             where (p.Name.Contains(Query) && p.Price >= LowerBoundPrice && p.Price <= UpperBoundPrice
                                    && p.Duration >= LowerBoundDuration && p.Duration <= UpperBoundDuration)
                             orderby p.Name
                             select p;

                    return (Query.Length == 1) ? q3.ToList().Where(x => x.Name[0].ToString().ToUpper() == Query[0].ToString().ToUpper()).ToList() : q3.ToList();
                }
                if (LowerBoundPrice == null && UpperBoundPrice == null &&
                    LowerBoundDuration != null && UpperBoundDuration != null && Query == null)
                {
                    var q4 = from p in dbContext.Courses
                             where (p.Duration >= LowerBoundDuration && p.Duration <= UpperBoundDuration)
                             orderby p.Duration
                             select p;

                    return q4.ToList();
                }
                if (LowerBoundPrice != null && UpperBoundPrice != null &&
                    LowerBoundDuration == null && UpperBoundDuration == null && Query == null)
                {
                    var q5 = from p in dbContext.Courses
                             where (p.Price >= LowerBoundPrice && p.Price <= UpperBoundPrice)
                             orderby p.Price
                             select p;

                    return q5.ToList();
                }
                if (LowerBoundPrice != null && UpperBoundPrice != null &&
                    LowerBoundDuration != null && UpperBoundDuration != null && Query == null)
                {
                    var q6 = from p in dbContext.Courses
                             where (p.Price >= LowerBoundPrice && p.Price <= UpperBoundPrice)
                             orderby p.Price
                             select p;

                    return q6.ToList();
                }
                else
                {
                    return null;
                }
            };
            
        }

        public void FindPopular()
        { 
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                
            }
        }
        
    }
}