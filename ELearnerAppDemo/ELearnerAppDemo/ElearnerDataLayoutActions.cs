using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearnerAppDemo
{
    public class ElearnerDataLayoutActions
    {
        //TODO: insert using
        public static void AddObjectToDb<T> (T obj, ElearnerContext dbContext) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentException("Object cannot be null.");
            }

            dbContext.Set<T>().Add(obj);
            dbContext.SaveChanges();
        }

        //TODO: insert using
        public static void RemoveObjectToDb<T> (T obj, ElearnerContext dbContext) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentException("Object cannot be null.");
            }

            dbContext.Set<T>().Remove(obj);
            dbContext.SaveChanges();
        }

        public static void UpdateAccountToDb (Account current, Account changes, ElearnerContext dbContext)
        {
            if (current == null && changes == null)
            {
                throw new ArgumentException("Accounts cannot be null.");
            }

            current.Email = changes.Email;
            current.Password = changes.Password;

            dbContext.SaveChanges();
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
        public static void SignUp (String name, string lastname, DateTime birthdate, string email, string password, ElearnerContext dbContext)
        {
            if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(lastname) || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password) || birthdate == null)
                throw new ArgumentException("Wrong Arguments!");
            else if (dbContext.Accounts.First(a => a.Email == email) != null)
                throw new InvalidOperationException("This user is already exists, choose a different email");

            AddObjectToDb<Account>(new Account() { Email = email, Password = password }, dbContext);

            int lastId = dbContext.Accounts.OrderByDescending(a => a.Id).Select(a => a.Id).First();

            AddObjectToDb<Student>(new Student() { Name = name, Lastname = lastname, Birthdate = birthdate, AccountId = lastId }, dbContext);

        }

        public static bool Login(string email, string password, ElearnerContext dbContext)
        {
            Account selected;
            try
            {
                selected = dbContext.Accounts.First(a => a.Email == email);
            }
            catch (Exception)
            {
                return false;
            }

            if (selected != null)
            {
                if (selected.Email == email && selected.Password == password)
                    return true;
            }

            return false;
        }

        public static bool PurchaseCourse(int courseId, int accountId, ElearnerContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
