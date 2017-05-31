using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElearnerApp.Models;
using ElearnerApp.ViewModels;

namespace ElearnerApp.Utilities
{
    public class ElearnerDataLayoutActions
    {
        public static void AddObjectToDb<T> (T obj) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentException("Object cannot be null.");
            }

            using (ElearnerContext dbContext = new ElearnerContext())
            {
                try
                {
                    dbContext.Set<T>().Add(obj);
                    dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void RemoveObjectToDb<T> (T obj) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentException("Object cannot be null.");
            }

            using (ElearnerContext dbContext = new ElearnerContext())
            {
                try
                {
                    dbContext.Set<T>().Remove(obj);
                    dbContext.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public static void UpdateAccountToDb (Account current, Account changed)
        {
            if (current == null && changed == null)
            {
                throw new ArgumentException("Accounts cannot be null.");
            }

            using (ElearnerContext dbContext = new ElearnerContext())
            {
                current.Email = changed.Email;
                current.Password = changed.Password;

                dbContext.SaveChanges();
            }
        }

        public static void UpdateStudentToDb (Student current, Student changes, ElearnerContext dbContext)
        {
            if (current == null && changes == null)
            {
                throw new ArgumentException("Accounts cannot be null.");
            }

            current.Name = changes.Name;
            current.Lastname = changes.Lastname;
            current.Birthdate = changes.Birthdate;

            dbContext.SaveChanges();
        }

        //TODO: Return bool
        public static bool SignUp (String name, string lastname, DateTime birthdate, string email, string password, Decimal deposit)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(lastname) 
                    || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password) || birthdate == null || deposit < 0)
                    return false;
                else if (dbContext.Accounts.FirstOrDefault(a => a.Email == email) != null)
                    return false;

                Account accountRecord = new Account()
                {
                    Email = email,
                    Password = password,
                };

                dbContext.Accounts.Add(accountRecord);
                dbContext.SaveChanges();

                int lastInsertedId = dbContext.Accounts.OrderByDescending(a => a.Id).Select(a => a.Id).First();

                Student studentRecord = new Student()
                {
                    Name = name,
                    Lastname = lastname,
                    Birthdate = birthdate,
                    AccountId = lastInsertedId
                };

                dbContext.Students.Add(studentRecord);
                dbContext.SaveChanges();

                BankAccount bankAccount = new BankAccount()
                {
                    AccountId = lastInsertedId,
                    Deposit = deposit
                };

                dbContext.BankAccounts.Add(bankAccount);
                dbContext.SaveChanges();

                return true;
            }
        }

        public static Account Login (string email, string password)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                Account result = dbContext.Accounts.Include(s => s.Student).FirstOrDefault(a => a.Email == email);

                if (result != null)
                {
                    if (result.Email == email && result.Password == password)
                        return result;
                }

                return null;
            }
        }

        public static Course GetCourseFromDb(int? courseId, string name)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                if (courseId != null && courseId > 0)
                {
                    return dbContext.Courses.Where(c => c.Id == courseId).Include(t =>t.Teacher).Include(c => c.Content).FirstOrDefault();
                }
                else if (!string.IsNullOrWhiteSpace(name))
                {
                    return dbContext.Courses.Where(c => c.Name == name).Include(t => t.Teacher).Include(c => c.Content).FirstOrDefault();
                }
                else
                    throw new ArgumentException("Wrong Arguments");
            }
        }

        public static string PurchaseCourse (int courseId, Student student, decimal courseCost)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                Subscription dbRecord = dbContext.Subscriptions
                    .Where(c => c.CourseId == courseId)
                    .Where(s => s.StudentId == student.AccountId)
                    .FirstOrDefault();
                if (dbRecord != null)
                    return "Course already has purchased!";

                BankAccount currentUserDeposit = dbContext.BankAccounts.Where(b => b.AccountId == student.AccountId).FirstOrDefault();

                if (currentUserDeposit.Deposit < courseCost)
                {
                    return "You dont have enough money!";
                }

                currentUserDeposit.Deposit -= courseCost;
                dbContext.SaveChanges();

                dbContext.Subscriptions.Add(new Subscription { CourseId = courseId, StudentId = student.AccountId });
                dbContext.SaveChanges();

                return "Course is successfully purchased!";
            }
        }

        public static List<CommentViewModel> GetCourseComments (int courseId)
        {
            List<CommentViewModel> comments = new List<CommentViewModel>();

            using (ElearnerContext dbContent = new ElearnerContext())
            {
                var results = dbContent.Subscriptions
                    .Where(c => c.CourseId == courseId)
                    .Include(s => s.Student)
                    .Select(x => new {Comment = x.Comment, StudentName = x.Student.Name+" "+ x.Student.Lastname, Grade = x.Grade } )
                    .ToList();

                foreach (var item in results)
                {
                    comments.Add(new CommentViewModel() { Comment = item.Comment, StudentName = item.StudentName, Grade = item.Grade });
                }
            }

            return comments;
        }

        public static Course GetContent(int id)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                Course course = dbContext.Courses.Where(c => c.Id == id).Include(c =>c.Content).FirstOrDefault();
                
                return course;
            }
        }

        public static Course GetQuiz(int id)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                Course course = dbContext.Courses.Where(c => c.Id == id).Include(c => c.Questions).FirstOrDefault();

                return course;
            }
        }


        //Dummy
        public void Method()
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                dbContext.Courses.Include(x => x.Subscriptions).GroupBy(x => x.Name);
            }
        }
    }
}
