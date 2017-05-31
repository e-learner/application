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
        public static void SignUp (String name, string lastname, DateTime birthdate, string email, string password, decimal deposit)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(lastname) || String.IsNullOrWhiteSpace(email) || String.IsNullOrWhiteSpace(password) || birthdate == null)
                    throw new ArgumentException("Wrong Arguments!");
                else if (dbContext.Accounts.FirstOrDefault(a => a.Email == email) != null)
                    throw new InvalidOperationException("This user is already exists, choose a different email");

                Account accountRecord = new Account()
                {
                    Email = email,
                    Password = password,
                    BankAccount = new BankAccount() { Deposit = deposit}
                };

                dbContext.Accounts.Add(accountRecord);

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
            }
        }

        public static bool Login(string email, string password)
        {
            using (ElearnerContext dbContext = new ElearnerContext())
            {
                Account selected = dbContext.Accounts.FirstOrDefault(a => a.Email == email);

                if (selected != null)
                {
                    if (selected.Email == email && selected.Password == password)
                        return true;
                }

                return false;
            }
        }

        public static bool PurchaseCourse(int courseId, int accountId, ElearnerContext dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
