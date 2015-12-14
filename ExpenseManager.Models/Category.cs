using ExpenseManager.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models
{
    public class Category
    {
        public Int64 ID { get; set; }
        
        [MaxLength(25)]
        [MinLength(5)]
        [Display(Name = "Category Name")]
        [RegularExpression("[A-Z|a-z]")]
        public string Name { get; set; }

        [AmountValidator("Plan")]
        public double Plan { get; set; }

        public string User { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
