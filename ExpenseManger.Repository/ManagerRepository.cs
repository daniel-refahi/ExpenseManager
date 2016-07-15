using ExpenseManager.Models;
using ExpenseManger.Model.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseManger.Repository
{
    // test
    public interface IManagerRepository
    {
        #region Expense
        IEnumerable<Expense> GetExpenses(string user, DateTime startDate, DateTime endDate);
        IEnumerable<Expense> GetExpenses(string user, int categoryId, DateTime startDate, DateTime endDate);
        OperationStatus AddExpense(Expense expense);
        OperationStatus UpdateExpense(Expense expense);
        OperationStatus DeleteExpense(long id);
        Expense GetExpense(long id);
        #endregion

        #region Category
        IEnumerable<CategoryDetail> GetCategories(string user, DateTime startDate, DateTime endDate);        
        Dictionary<long, string> GetCategoriesNames(string user);
        OperationStatus AddCategory(Category category);
        OperationStatus UpdateCategory(Category category);
        OperationStatus DeleteCategory(long id);
        Category GetCategory(long id);
        #endregion

    }

    public class ManagerRepository : RepositoryBase<ExpenseManagerContext>, IManagerRepository
    {
        #region Expense
        public OperationStatus AddExpense(Expense expense)
        {

            try
            {
                Add(expense);
                Save();
                return new OperationStatus { Status = true };
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromSystemException("Error on adding expense.", ex);
            }
        }

        public OperationStatus UpdateExpense(Expense expense)
        {
            try
            {
                Expense updatedExpense = GetExpense(expense.ID);
                if (updatedExpense == null)
                    return new OperationStatus { Status = false, Message = "Expense record doesn't exist." };

                updatedExpense.ExpenseDate = expense.ExpenseDate;
                updatedExpense.Description = expense.Description;
                updatedExpense.Amount = expense.Amount;

                Update(expense, updatedExpense);
                Save();
                return new OperationStatus { Status = true };
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromSystemException("Error on updating expense.", ex);
            }
        }

        public OperationStatus DeleteExpense(long id)
        {
            try
            {
                Delete<Expense>(e => e.ID == id);
                Save();
                return new OperationStatus { Status = true };

            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromSystemException("Error on deleting expense.", ex);
            }
        }

        public IEnumerable<Expense> GetExpenses(string user, DateTime startDate, DateTime endDate)
        {
            try
            {
                return GetList<Expense>(e => e.ExpenseDate >= startDate &&
                                             e.ExpenseDate < endDate &&
                                             e.User == user);
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public IEnumerable<Expense> GetExpenses(string user, int categoryId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return GetList<Expense>(e => e.ExpenseDate >= startDate &&
                                             e.ExpenseDate < endDate &&
                                             e.Category.ID == categoryId &&
                                             e.User == user).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public Expense GetExpense(long id)
        {
            try
            {
                return Get<Expense>(e => e.ID == id);
            }
            catch (Exception)
            {
                return new Expense();
            }
            
        }

        #endregion

        #region Category

        public OperationStatus AddCategory(Category category)
        {
            try
            {
                Add(category);
                OperationStatus os =  Save();
                if (os.Message != null && os.Message.Contains("duplicate key"))
                    return new OperationStatus()
                    {
                        Message = "Category name already exists in the system.",
                        Status = false
                    };
                else
                    return os;
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromSystemException("Error on adding category.", ex);
            }
        }

        public OperationStatus DeleteCategory(long id)
        {
            try
            {
                //we need deleting all expenses in this category first.                
                Delete<Expense>(e => e.Category.ID == id);

                Delete<Category>(c => c.ID == id);
                Save();
                return new OperationStatus { Status = true };
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromSystemException("Error on deleting category.", ex);
            }
        }

        public OperationStatus UpdateCategory(Category category)
        {
            try
            {
                Category updatedCategory = GetCategory(category.ID);
                if (updatedCategory == null)
                    return new OperationStatus { Status = false, Message = "Category dosn't exist." };

                updatedCategory.Name = category.Name;
                updatedCategory.Plan = category.Plan;
                Update(category, updatedCategory);
                Save();                
                return new OperationStatus { Status = true };
            }
            catch (Exception ex)
            {
                return OperationStatus.CreateFromSystemException("Error on updating category.", ex);
            }
        }

        public IEnumerable<CategoryDetail> GetCategories(string user, DateTime startDate, DateTime endDate)
        {
            try
            {

                //var expenses = GetList<Expense>(e => e.User == user);
                //var result = GetList<Category>(c => c.User == user)
                //                                        .Select(c =>
                //                                          new CategoryDetail()
                //                                          {
                //                                              ID = c.ID,
                //                                              CategoryName = c.Name,
                //                                              Plan = c.Plan,
                //                                              TotalExpense = expenses.Where(e => e.Category.ID == c.ID).Sum(e => e.Amount)
                //                                          });

                var result = GetList<Category>(c => c.User == user)
                                                        .Select(c =>
                                                          new CategoryDetail()
                                                          {
                                                              ID = c.ID,
                                                              CategoryName = c.Name,
                                                              Plan = c.Plan,
                                                              TotalExpense = GetList<Expense>(e => e.Category.ID == c.ID).Sum(e => e.Amount)
                                                          });

                return result;

            }
            catch (Exception ex)
            {
                return null;
            }
                       
        }

        public Dictionary<long,string> GetCategoriesNames(string user)
        {
            try
            {
                return GetList<Category>(c => c.User == user).ToDictionary(c => c.ID, c => c.Name);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        
        public Category GetCategory(long id)
        {
            try
            {
                return Get<Category>(c => c.ID == id);
                    
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

    }


}
