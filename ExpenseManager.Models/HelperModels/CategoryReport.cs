using System.Collections.Generic;

namespace ExpenseManger.Model.HelperModels
{
    public class CategoryReport
    {
        public string CategoryName { get; set; }
        public Dictionary<string, double> ExpenseMap { get; set; }
        public Dictionary<string, double> PlanMap { get; set; }
    }
}
