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
        #region Expense
        List<Expense> GetExpenses(int year, int month);
        List<Expense> GetExpenses(decimal categoryId, int year, int month);
        OperationStatus AddExpense(Expense expense);
        OperationStatus UpdateExpense(Expense expense);
        OperationStatus DeleteExpense(decimal id);
        #endregion

        #region Category
        List<CategoryDetail> GetCategories(int year, int month);
        OperationStatus AddCategory(Category category);
        OperationStatus UpdateCategory(Category category);
        OperationStatus DeleteCategory(decimal id);
        #endregion

    }

    public class ManagerRepository : RepositoryBase<ExpenseManager>, IManagerRepository
    {
        #region Expense
        public OperationStatus AddExpense(Expense expense)
        {
            using (DataContext)
            {
                DataContext.Expenses.Add(expense);
                try
                {
                    DataContext.SaveChanges();
                    return new OperationStatus { Status = true };
                }
                catch (Exception ex)
                {
                    return OperationStatus.CreateFromException("Error on adding expense.", ex);
                }
            }
        }

        public OperationStatus UpdateExpense(Expense expense)
        {
            using (DataContext)
            {
                DataContext.Entry(expense).State = System.Data.Entity.EntityState.Modified;
                try
                {
                    DataContext.SaveChanges();
                    return new OperationStatus { Status = true };
                }
                catch (Exception ex)
                {
                    return OperationStatus.CreateFromException("Error on updating expense.", ex);
                }
            }
        }

        public OperationStatus DeleteExpense(decimal id)
        {
            using (DataContext)
            {                               
                try
                {
                    Expense expense = DataContext.Expenses
                                         .Where(e => e.ID == id)
                                         .FirstOrDefault();
                    DataContext.Entry(expense).State = System.Data.Entity.EntityState.Deleted;
                    DataContext.SaveChanges();
                    return new OperationStatus { Status = true };
                }
                catch (Exception ex)
                {
                    return OperationStatus.CreateFromException("Error on deleting expense.", ex);
                }
            }
        }

        public List<Expense> GetExpenses(int year, int month)
        {
            using (DataContext)
            {
                return DataContext.Expenses
                        .Where(e => e.ExpenseDate.Month == month && 
                                    e.ExpenseDate.Year == year)
                        .ToList();
            }
        }

        public List<Expense> GetExpenses(decimal categoryId, int year, int month)
        {
            return GetExpenses(year, month)
                    .Where(e => e.Category.ID == categoryId)
                    .ToList();                    
        }

        #endregion

        #region Category
        public OperationStatus AddCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public OperationStatus DeleteCategory(decimal id)
        {
            throw new NotImplementedException();
        }
        
        public List<CategoryDetail> GetCategories(int year, int month)
        {
            throw new NotImplementedException();
        }
        
        public OperationStatus UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
        #endregion
        
    }


}
