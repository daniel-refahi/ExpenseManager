using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManger.Repository
{
    public class ExpenseManager:DbContext, IDisposedTracker
    {
        
        public ExpenseManager() : base("RepositoryConnection")
        {                        
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }

        public bool IsDisposed { get; set; }
        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }
}
