using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearnerApp
{
    public class BankAccount
    {
        public int Id { get; set; }
        public decimal Deposit { get; set; }

        public Account Account { get; set; }
    }
}
