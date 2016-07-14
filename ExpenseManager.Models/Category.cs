using ExpenseManager.Models.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseManager.Models
{
    public class Category
    {
        public Int64 ID { get; set; }
        
        [MaxLength(25, ErrorMessage = "Maximum lenght for category name is 25")]
        [MinLength(3, ErrorMessage ="Minimum lenght for category name is 3")]        
        [Index(IsUnique = true)]
        [Display(Name = "Category Name")]
        [RegularExpression("^[a-zA-Z0-9-_.]+$", ErrorMessage = "Category name has unacceptable characters.")]
        public string Name { get; set; }

        [AmountValidator("Plan")]        
        public double Plan { get; set; }

        public string User { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
    }
}
