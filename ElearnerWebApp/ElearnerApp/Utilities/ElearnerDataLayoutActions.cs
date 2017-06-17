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

        public static void UpdateCourse(Course changedCourse)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                Course oldCourse = dbContext.Courses.Where(c => c.Id == changedCourse.Id).Include(c => c.Content).FirstOrDefault();

                oldCourse.Name = changedCourse.Name;
                oldCourse.Price = changedCourse.Price;
                oldCourse.Description = changedCourse.Description;
                oldCourse.Duration = changedCourse.Duration;
                oldCourse.Content.TextContent = changedCourse.Content.TextContent;
                oldCourse.Content.VideoUrl = changedCourse.Content.VideoUrl;

                dbContext.SaveChanges();
            }
        }

        public static Account SignUp (String name, string lastname, DateTime birthdate, string email, string password, Decimal deposit)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(lastname) 
                    || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password) || birthdate == null || deposit < 0)
                    return null;
                else if (dbContext.Accounts.FirstOrDefault(a => a.Email == email) != null)
                    return null;

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

                Account result = new Account();
                result = accountRecord;
                result.Student = studentRecord;
                result.BankAccount = bankAccount;

                return result;
            }
        }

        public static Account SignUpTeacher(SignUpTeacherViewModel teacherViewModel)
        {
            Account createdTeacher;

            if (teacherViewModel == null || string.IsNullOrWhiteSpace(teacherViewModel.TeacherAccount.Email) || string.IsNullOrWhiteSpace(teacherViewModel.TeacherAccount.Password) || teacherViewModel.TeacherAccount.BankAccount.Deposit < 0
               || string.IsNullOrWhiteSpace(teacherViewModel.TeachingCourse.Name) || string.IsNullOrWhiteSpace(teacherViewModel.TeachingCourse.Description) || teacherViewModel.TeachingCourse.Duration < 0)
                return null;

            using (ElearnerContext dbContext = new ElearnerContext())
            {
                createdTeacher = dbContext.Accounts.Add(teacherViewModel.TeacherAccount);
                
                dbContext.Courses.Add(teacherViewModel.TeachingCourse);
                dbContext.SaveChanges();
            }

            return createdTeacher;
        }

        public static Account HasAccount(string email, string password)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                Account result = dbContext.Accounts.Include(s => s.Student).FirstOrDefault(a => a.Email == email);

                if (result != null)
                {
                    if (result.Email == email && result.Password == password)
                    {
                        if (dbContext.Students.Where(s => s.AccountId == result.Id).FirstOrDefault() != null)
                        {
                            return dbContext.Accounts.Include(s => s.Student).Include(b => b.BankAccount).FirstOrDefault(a => a.Email == email);
                        }
                        else
                        {
                            return dbContext.Accounts.Include(t => t.Teacher).Include(b => b.BankAccount).FirstOrDefault(a => a.Email == email);
                        }
                    }
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

        public static IList<Course> GetCourseByTeacher(int teacherId)
        {
            IList<Course> results;
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                results = dbContext.Courses.Where(c => c.TeacherId == teacherId).ToList();
            }

            return results;
        }

        public static Course GetFullCourseDetails (int courseId)
        {
            Course results;
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                results = dbContext.Courses.Where(c => c.Id == courseId).Include(c =>c.Content).Include(q => q.Questions).FirstOrDefault();
            }

            return results;
        }

        public static string PurchaseCourse(int courseId, int accountid, decimal courseCost)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                Subscription dbRecord = dbContext.Subscriptions
                    .Where(c => c.CourseId == courseId)
                    .Where(s => s.StudentId == accountid)
                    .FirstOrDefault();
                if (dbRecord != null)
                    return "Course already has purchased!";

                BankAccount currentUserDeposit = dbContext.BankAccounts.Where(b => b.AccountId == accountid).FirstOrDefault();

                if (currentUserDeposit.Deposit < courseCost)
                {
                    return"You dont have enough money!";
                }

                currentUserDeposit.Deposit -= courseCost;
                dbContext.SaveChanges();

                BankAccount teacherDeposit = dbContext.BankAccounts
                    .Where(b => b.AccountId == (dbContext.Courses.Where( c => c.Id == courseId).Select(t => t.TeacherId).FirstOrDefault()))
                    .FirstOrDefault();
                teacherDeposit.Deposit += courseCost;
                dbContext.SaveChanges();

                dbContext.Subscriptions.Add(new Subscription { CourseId = courseId, StudentId = accountid });
                dbContext.SaveChanges();

                return "Course is successfully purchased!";
            }
        }

        public static IList<Subscription> GetStudentSubscriptions(int studentId)
        {
            IList<Subscription> subscriptions;
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                subscriptions = dbContext.Subscriptions.Where(x => x.StudentId == studentId).Include(x => x.Course).ToList();
            }
            return subscriptions;
        }

        public static bool HasStudentThisCourse(int studentId, int CourseId)
        {
            bool result;
            using (ElearnerContext dbContent = new ElearnerContext())
            {
                result = dbContent.Subscriptions
                    .Where(x => x.StudentId == studentId)
                    .Where(x => x.CourseId == CourseId)
                    .FirstOrDefault() != null ? true : false;
            }

            return result;
        }

        public static decimal UpdateUserDeposit(int accountId)
        {
            decimal deposit;
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                deposit = dbContext.BankAccounts.Where(b => b.AccountId == accountId).Select(d => d.Deposit).FirstOrDefault();
            }

            return deposit;
        }

        public static decimal AddMoneyToUser(int accountId, decimal amount)
        {
            BankAccount bankAccount;
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                bankAccount = dbContext.BankAccounts.Where(b => b.AccountId == accountId).FirstOrDefault();
                bankAccount.Deposit += amount;

                dbContext.SaveChanges();
            }

            return bankAccount.Deposit;
        }

        public static List<CommentViewModel> GetCourseComments (int courseId)
        {
            List<CommentViewModel> comments = new List<CommentViewModel>();

            using (ElearnerContext dbContent = new ElearnerContext())
            {
                var results = dbContent.Subscriptions
                    .Where(c => c.CourseId == courseId)
                    .Where(c => c.Comment != null && c.Comment.Trim() !="")
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
        
        public static List<Course> GetMostPopular()
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                var popularCourses = dbContext.Courses.SqlQuery("SELECT TOP 3 Id,Name,Duration,Price,TeacherId,Description " +
                                                                "FROM Courses INNER JOIN Subscriptions ON Courses.Id = Subscriptions.CourseId " +
                                                                "GROUP BY Id,Name,Duration,Price,TeacherId,Description " +
                                                                "ORDER BY COUNT(Subscriptions.StudentId) DESC").ToList();
                return popularCourses;
            }
            
        }

        public static List<Course> GetFreeCourses()
        {
            Random r = new Random();

            using (ElearnerContext dbContext = new ElearnerContext())
            {
                var query = dbContext.Courses.Where(x => x.Price == 0).ToList();
                List<Course> freeCourses = query.OrderBy(x => r.Next()).Take(3).ToList();

                return freeCourses;
            }
        }

        public static List<Teacher> GetTeachers ()
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                List<Teacher> teacherList = dbContext.Teachers.Include(a => a.Account).ToList();

                return teacherList;
            }
        }

        public static void UpdateGradeToDb (byte? grade, int studentid, int courseid)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                var query = dbContext.Subscriptions.Where(x => x.StudentId == studentid && x.CourseId == courseid).FirstOrDefault();
                query.Grade = grade;
                dbContext.SaveChanges();
            }
        }

        public static void SaveSubscriptionChanges(int courseId, int accountId, string comment, byte? rating)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                // If you have composite primary key, then pass key values in the order they defined in model:
                var subscriptionInDB = dbContext.Subscriptions.Find(courseId, accountId);

                if (!String.IsNullOrEmpty(comment))
                    subscriptionInDB.Comment = comment;

                if (rating != null)
                    subscriptionInDB.Rate = rating;

                try
                {
                    dbContext.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("could not reach the Database");
                }

            }
        }

        public static double? GetCourseRating(int courseId)
        {
            double? avg;
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                avg = dbContext.Subscriptions.Where(s => s.CourseId == courseId && s.Rate != null).Average(s => s.Rate);
            }

            return avg;
        }
    }
}
