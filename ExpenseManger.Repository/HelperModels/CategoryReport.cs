using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManger.Repository.HelperModels
{
    public class CategoryReport
    {
        public string CategoryName { get; set; }
        public Dictionary<int, double> ExpenseMap { get; set; }
        public Dictionary<int, double> PlanMap { get; set; }
    }
}
