using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models
{
    public class Expense
    {
        public Int64 ID { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expense Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; }
        public string User { get; set; }

        public virtual Category Category { get; set; }

    }
}
