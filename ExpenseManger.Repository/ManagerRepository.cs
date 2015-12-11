using ExpenseManager.Models;
using ExpenseManger.Repository.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManger.Repository
{
    public interface IManagerRepository
    {
        // some test
        List<Expense> GetExpenses(int year, int month);
        List<Expense> GetExpenses(int categoryId, int year, int month);
        List<CategoryDetail> GetCategories(int year, int month);
    }

    public class ManagerRepository : RepositoryBase<ExpenseManager>, IManagerRepository
    {
        public List<CategoryDetail> GetCategories(int year, int month)
        {
            throw new NotImplementedException();
        }

        public List<Expense> GetExpenses(int year, int month)
        {
            throw new NotImplementedException();
        }

        public List<Expense> GetExpenses(int categoryId, int year, int month)
        {
            throw new NotImplementedException();
        }
    }


}
