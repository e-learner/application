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
                //ElearnerDataLayoutActions.SignUp("Iasonas", "Stamatopoulos", new DateTime(1991, 5, 7), "iasonas@stamat.gr", "400", dbContext);
                //bool result = ElearnerDataLayoutActions.Login("iasonas@stadmat.gr", "400", dbContext);


                var student = (from s in dbContext.Students select s).First();
                Console.WriteLine(student);
            }

            Console.WriteLine("Press a key to continue...");
            Console.ReadKey();
        }
    }
}
