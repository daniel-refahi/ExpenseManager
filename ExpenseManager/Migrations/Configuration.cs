namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExpenseManager.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExpenseManager.Models.ApplicationDbContext context)
        {
            
        }
    }
}


//protected override void Seed(ExpenseManager.Models.ApplicationDbContext context)
//{

//    AddUserAndRole(context);
//}
////  This method will be called after migrating to the latest version.

//bool AddUserAndRole(ApplicationDbContext context)
//{
//    IdentityResult ir;
//    var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
//    ir = rm.Create(new IdentityRole("Premium"));
//    var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
//    return ir.Succeeded;
//    //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
//    //  to avoid creating duplicate seed data. E.g.
//    //
//    //    context.People.AddOrUpdate(
//    //      p => p.FullName,
//    //      new Person { FullName = "Andrew Peters" },
//    //      new Person { FullName = "Brice Lambson" },
//    //      new Person { FullName = "Rowan Miller" }
//    //    );
//    //
//}
//    }