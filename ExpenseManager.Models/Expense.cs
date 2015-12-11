using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class Expense
    {
        public decimal ID { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string User { get; set; }

        public virtual Category Category { get; set; }

    }
}
