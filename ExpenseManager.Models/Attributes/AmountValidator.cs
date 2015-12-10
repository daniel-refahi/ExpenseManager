using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Models.Attributes
{
    public class AmountValidator : ValidationAttribute
    {        
        public AmountValidator(string amountType)
        {
            ErrorMessage = string.Format("Your {0} cannot be less than or equal to zero.", amountType); 
        }

        public override bool IsValid(object value)
        {
            double amount;
            if (double.TryParse(value.ToString(), out amount))
            {
                if (amount <= 0)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
    }
}
