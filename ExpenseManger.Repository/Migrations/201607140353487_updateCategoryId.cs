namespace ExpenseManger.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCategoryId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Expenses", "Category_ID", "dbo.Categories");
            DropIndex("dbo.Expenses", new[] { "Category_ID" });
            RenameColumn(table: "dbo.Expenses", name: "Category_ID", newName: "CategoryID");
            AlterColumn("dbo.Expenses", "CategoryID", c => c.Long(nullable: false));
            CreateIndex("dbo.Expenses", "CategoryID");
            AddForeignKey("dbo.Expenses", "CategoryID", "dbo.Categories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Expenses", new[] { "CategoryID" });
            AlterColumn("dbo.Expenses", "CategoryID", c => c.Long());
            RenameColumn(table: "dbo.Expenses", name: "CategoryID", newName: "Category_ID");
            CreateIndex("dbo.Expenses", "Category_ID");
            AddForeignKey("dbo.Expenses", "Category_ID", "dbo.Categories", "ID");
        }
    }
}
