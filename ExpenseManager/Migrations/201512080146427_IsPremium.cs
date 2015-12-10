namespace ExpenseManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsPremium : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsPremium", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsPremium");
        }
    }
}
