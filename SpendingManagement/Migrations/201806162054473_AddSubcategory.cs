namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSubcategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subcategories", "Category_Id", c => c.Byte());
            CreateIndex("dbo.Subcategories", "Category_Id");
            AddForeignKey("dbo.Subcategories", "Category_Id", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subcategories", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Subcategories", new[] { "Category_Id" });
            DropColumn("dbo.Subcategories", "Category_Id");
        }
    }
}
