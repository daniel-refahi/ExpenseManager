namespace ExpenseManger.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 25),
                        Plan = c.Double(nullable: false),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        Amount = c.Double(nullable: false),
                        ExpenseDate = c.DateTime(nullable: false),
                        User = c.String(),
                        Category_ID = c.Long(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_ID)
                .Index(t => t.Category_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "Category_ID", "dbo.Categories");
            DropIndex("dbo.Expenses", new[] { "Category_ID" });
            DropTable("dbo.Expenses");
            DropTable("dbo.Categories");
        }
    }
}
