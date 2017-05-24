using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearnerApp
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
                dbContext.Set<T>().Add(obj);
                dbContext.SaveChanges();
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
                dbContext.Set<T>().Remove(obj);
                dbContext.SaveChanges();
            }
        }

        public static void UpdateObjectToDb<T> (T obj) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentException("Object cannot be null.");
            }

            using (ElearnerContext dbContext = new ElearnerContext())
            {
                dbContext.Set<T>().Add(obj);
                dbContext.SaveChanges();
            }
        }

    }
}
