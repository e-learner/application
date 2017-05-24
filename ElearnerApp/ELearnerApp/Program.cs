using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearnerApp
{
    class Program
    {
        static void Main (string[] args)
        {

            using (ElearnerContext dbContext = new ElearnerContext())
            {
                var account = dbContext.Accounts.Single(a => a.Id == 5);

                dbContext.Accounts.Attach(account);
                ElearnerDataLayoutActions.RemoveObjectToDb(account);

            }
                Console.ReadKey();

            
        }
    }
}
