﻿using ExpenseManager.Models;
using ExpenseManger.Model.HelperModels;
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
        List<Expense> GetExpenses(string user, DateTime startDate, DateTime endDate);
        List<Expense> GetExpenses(string user, int categoryId, DateTime startDate, DateTime endDate);
        OperationStatus AddExpense(Expense expense);
        OperationStatus UpdateExpense(Expense expense);
        OperationStatus DeleteExpense(Int64 id);
        Expense GetExpense(Int64 id);    
        #endregion

        #region Category
        List<CategoryDetail> GetCategories(string user, DateTime startDate, DateTime endDate);
        List<string> GetCategoriesNames(string user);
        OperationStatus AddCategory(Category category);
        OperationStatus UpdateCategory(Category category);
        OperationStatus DeleteCategory(Int64 id);
        Category GetCategory(Int64 id);        
        #endregion

    }

    public class ManagerRepository : RepositoryBase<ExpenseManagerContext>, IManagerRepository
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
                    return OperationStatus.CreateFromSystemException("Error on adding expense.", ex);
                }
            }
        }

        public OperationStatus UpdateExpense(Expense expense)
        {
            using (DataContext)
            {                
                try
                {
                    Expense updatedExpense = GetExpense(expense.ID);
                    if (updatedExpense == null)
                        return new OperationStatus { Status = false, Message = "Expense record doesn't exist." };

                    updatedExpense.ExpenseDate = expense.ExpenseDate;
                    updatedExpense.Description = expense.Description;
                    updatedExpense.Amount = expense.Amount;

                    DataContext.Entry(updatedExpense).State = System.Data.Entity.EntityState.Modified;
                    DataContext.SaveChanges();
                    return new OperationStatus { Status = true };
                }
                catch (Exception ex)
                {
                    return OperationStatus.CreateFromSystemException("Error on updating expense.", ex);
                }
            }
        }

        public OperationStatus DeleteExpense(Int64 id)
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
                    return OperationStatus.CreateFromSystemException("Error on deleting expense.", ex);
                }
            }
        }

        public List<Expense> GetExpenses(string user, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (DataContext)
                {
                    return DataContext.Expenses
                            .Where(e => e.ExpenseDate >= startDate &&
                                        e.ExpenseDate < endDate &&
                                        e.User == user)
                            .ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }            
        }

        public List<Expense> GetExpenses(string user, int categoryId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return GetExpenses(user, startDate, endDate)
                    .Where(e => e.Category.ID == categoryId)
                    .ToList();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public Expense GetExpense(Int64 id)
        {
            try
            {
                using (DataContext)
                {
                    return DataContext.Expenses.Find(id);
                }
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
            using (DataContext)
            {
                // category name must be unique for each user.
                var currentCategory = DataContext.Categories
                                         .Where(c => c.Name == category.Name &&
                                                c.User == category.User);
                if (currentCategory.Count() != 0)
                    return OperationStatus.CreateFromUserException("Category already exists.");
                
                DataContext.Categories.Add(category);
                try
                {
                    DataContext.SaveChanges();
                    return new OperationStatus { Status = true };
                }
                catch (Exception ex)
                {
                    return OperationStatus.CreateFromSystemException("Error on adding category.", ex);
                }
            }
        }

        public OperationStatus DeleteCategory(Int64 id)
        {
            using (DataContext)
            {
                try
                {
                    // deleting all expenses in this category first.
                    DataContext.Expenses.RemoveRange(
                        DataContext.Expenses.Where(e=> e.Category.ID == id));

                    var category = DataContext.Categories
                                                  .Where(c => c.ID == id)
                                                  .FirstOrDefault();

                    DataContext.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                    DataContext.SaveChanges();
                    return new OperationStatus { Status = true };
                }
                catch (Exception ex)
                {
                    return OperationStatus.CreateFromSystemException("Error on deleting category.", ex);
                }
            }
        }

        public OperationStatus UpdateCategory(Category category)
        {
            using (DataContext)
            {                
                try
                {
                    Category updatedCategory = GetCategory(category.ID);
                    if (updatedCategory == null)
                        return new OperationStatus { Status = false, Message = "Category dosn't exist." };

                    updatedCategory.Name = category.Name;
                    updatedCategory.Plan = category.Plan;

                    DataContext.Entry(updatedCategory).State = System.Data.Entity.EntityState.Modified;
                    DataContext.SaveChanges();
                    return new OperationStatus { Status = true };
                }
                catch (Exception ex)
                {
                    return OperationStatus.CreateFromSystemException("Error on updating category.", ex);
                }
            }
        }

        public List<CategoryDetail> GetCategories(string user, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (DataContext)
                {
                    return DataContext.Categories
                                .Where(c => c.User == user)
                                .Select(c =>
                                    new CategoryDetail()
                                    {
                                        ID = c.ID,
                                        CategoryName = c.Name,
                                        Plan = c.Plan,
                                        TotalExpense = DataContext.Expenses
                                            .Where(e => e.Category.ID == c.ID)
                                            .Sum(e=> e.Amount)
                                    })
                                .ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
                       
        }

        public List<string> GetCategoriesNames(string user)
        {
            try
            {
                using (DataContext)
                {
                    return DataContext.Categories
                                .Where(c => c.User == user)
                                .Select(c => c.Name)
                                .ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
            
        }
        
        public Category GetCategory(Int64 id)
        {
            try
            {
                using (DataContext)
                {
                    return DataContext.Categories.Find(id);
                }
                    
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

    }


}
