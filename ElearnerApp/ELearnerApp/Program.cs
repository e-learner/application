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
            ELearnerContext context = new ELearnerContext();

            var accounts = (from a in context.Teachers select a.Name).ToList();

            foreach (var item in accounts)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();
        }
    }
}
