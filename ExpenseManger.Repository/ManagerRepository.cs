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
        OperationStatus DeleteExpense(long id, string userId);
        Expense GetExpense(long id, string userId);
        #endregion

        #region Category
        IEnumerable<CategoryDetail> GetCategories(string user, DateTime startDate, DateTime endDate);        
        Dictionary<long, string> GetCategoriesNames(string user);
        OperationStatus AddCategory(Category category);
        OperationStatus UpdateCategory(Category category);
        OperationStatus DeleteCategory(long id, string userId);
        Category GetCategory(long id, string userId);
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
                Expense updatedExpense = GetExpense(expense.ID, expense.User);
                if (updatedExpense == null)
                    return new OperationStatus { Status = false, Message = "Expense record doesn't exist or You are not autorized to access it." };

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

        public OperationStatus DeleteExpense(long id, string userId)
        {
            try
            {
                Expense expense = Get<Expense>(e => e.ID == id && e.User == userId);
                // this means the requester is not the owner of the record.
                if(expense == null)
                    return new OperationStatus { Status = false, Message = "You are not autorized to access this record!" };

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

        public Expense GetExpense(long id, string userId)
        {
            try
            {
                return Get<Expense>(e => e.ID == id && e.User == userId);
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

        public OperationStatus DeleteCategory(long id, string userId)
        {
            try
            {
                Category category = Get<Category>(c => c.ID == id && c.User == userId);
                // this means the requester is not the owner of the record.
                if (category == null)
                    return new OperationStatus { Status = false, Message = "You are not autorized to access this record!" };

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
                Category updatedCategory = GetCategory(category.ID, category.User);
                if (updatedCategory == null)
                    return new OperationStatus { Status = false, Message = "Category record doesn't exist or You are not autorized to access it." };

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

        /// <summary>
        /// Return Categories with sum of expenses in that category
        /// </summary>
        /// <param name="user"></param>
        /// <param name="startDate">Start date for summing up the expenses in this category</param>
        /// <param name="endDate">End date for summing up the expenses in this category</param>
        /// <returns></returns>
        public IEnumerable<CategoryDetail> GetCategories(string user, DateTime startDate, DateTime endDate)
        {
            try
            {

                // less query but it grabs all the expenses records
                var expenses = GetList<Expense>(e => e.User == user);
                var result = GetList<Category>(c => c.User == user)
                                                        .Select(c =>
                                                          new CategoryDetail()
                                                          {
                                                              ID = c.ID,
                                                              CategoryName = c.Name,
                                                              Plan = c.Plan,
                                                              TotalExpense = expenses.Where(e => e.Category.ID == c.ID).Sum(e => e.Amount)
                                                          });

                // more query but it doesn't grab all the expenes records
                //var result = GetList<Category>(c => c.User == user)
                //                                        .Select(c =>
                //                                          new CategoryDetail()
                //                                          {
                //                                              ID = c.ID,
                //                                              CategoryName = c.Name,
                //                                              Plan = c.Plan,
                //                                              TotalExpense = GetList<Expense>(e => e.Category.ID == c.ID).Sum(e => e.Amount)
                //                                          });

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
        
        public Category GetCategory(long id,string userId)
        {
            try
            {
                return Get<Category>(c => c.ID == id && c.User == userId);
                    
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

    }


}
