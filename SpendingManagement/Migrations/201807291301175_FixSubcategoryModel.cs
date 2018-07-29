namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixSubcategoryModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Subcategories", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Subcategories", "CategoryId", c => c.Int(nullable: false));
        }
    }
}
