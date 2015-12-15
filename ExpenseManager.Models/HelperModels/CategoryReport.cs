using System.Collections.Generic;

namespace ExpenseManger.Model.HelperModels
{
    public class CategoryReport
    {
        public string CategoryName { get; set; }
        public Dictionary<int, double> ExpenseMap { get; set; }
        public Dictionary<int, double> PlanMap { get; set; }
    }
}
