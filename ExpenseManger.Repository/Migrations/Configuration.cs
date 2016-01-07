namespace ExpenseManger.Repository.Migrations
{
    using ExpenseManager.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExpenseManger.Repository.ExpenseManagerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExpenseManger.Repository.ExpenseManagerContext context)
        {
            Category rent = new Category()
            {
                Name = "Rent",
                Plan = 500,
                User = "f887ffcc-c58f-460f-9952-fa2d089af26e"
            };

            Category house = new Category()
            {
                Name = "House",
                Plan = 1200,
                User = "f887ffcc-c58f-460f-9952-fa2d089af26e"
            };

            Category car = new Category()
            {
                Name = "Car",
                Plan = 250,
                User = "f887ffcc-c58f-460f-9952-fa2d089af26e"
            };

            context.Categories.AddOrUpdate(rent);
            context.Categories.AddOrUpdate(house);
            context.Categories.AddOrUpdate(car);

            for (int counter = 0; counter < 10; counter++)
            {
                Expense rentExpsnse = new Expense()
                {
                    Amount = 200 + counter,
                    Category = rent,
                    Description = "Rent expense number " + counter,
                    ExpenseDate = DateTime.Now.AddMonths(-counter),
                    User = "f887ffcc-c58f-460f-9952-fa2d089af26e"
                };

                Expense houseExpsnse = new Expense()
                {
                    Amount = 150 + counter,
                    Category = house,
                    Description = "House expense number " + counter,
                    ExpenseDate = DateTime.Now.AddMonths(-counter),
                    User = "f887ffcc-c58f-460f-9952-fa2d089af26e"
                };

                Expense carExpsnse = new Expense()
                {
                    Amount = 10 + counter,
                    Category = car,
                    Description = "Car expense number " + counter,
                    ExpenseDate = DateTime.Now.AddMonths(-counter),
                    User = "f887ffcc-c58f-460f-9952-fa2d089af26e"
                };

                context.Expenses.AddOrUpdate(rentExpsnse);
                context.Expenses.AddOrUpdate(houseExpsnse);
                context.Expenses.AddOrUpdate(carExpsnse);
            }

            context.SaveChanges();
        }
    }
}
