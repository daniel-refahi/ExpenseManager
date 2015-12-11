namespace ExpenseManager.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using InfraStructure;

    internal sealed class Configuration : DbMigrationsConfiguration<ExpenseManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "ExpenseManager.Models.ApplicationDbContext";
        }

        protected override void Seed(ExpenseManager.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);            
        }

        bool AddUserAndRole(ExpenseManager.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole> (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole(Helpers.FullAccessProfile));
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));            
            return ir.Succeeded;
        }
    }
}
