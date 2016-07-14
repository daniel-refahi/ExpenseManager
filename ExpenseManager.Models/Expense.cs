using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models
{
    public class Expense
    {
        public long ID { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expense Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpenseDate { get; set; }
        public string User { get; set; }
        
        public long CategoryID { get; set; }
        public virtual Category Category { get; set; }

    }
}
