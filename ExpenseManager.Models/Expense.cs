using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class Expense
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int User { get; set; }

        public virtual Category Category { get; set; }

    }
}
