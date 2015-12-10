using ExpenseManager.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseManager.Models
{
    public class Category
    {
        public int ID { get; set; }
        
        [MaxLength(25)]
        [MinLength(5)]
        [Display(Name = "Category Name")]
        [RegularExpression("[A-Z|a-z]")]
        public string Name { get; set; }

        [AmountValidator("Plan")]
        public double Plan { get; set; }

        public int User { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
