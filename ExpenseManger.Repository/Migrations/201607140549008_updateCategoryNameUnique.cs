namespace ExpenseManger.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCategoryNameUnique : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Categories", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Categories", new[] { "Name" });
        }
    }
}
