namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBoolColumntToCategoryEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "isRevenue", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "isRevenue");
        }
    }
}
