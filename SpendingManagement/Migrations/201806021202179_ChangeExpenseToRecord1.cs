namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeExpenseToRecord1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Records", "IsRevenue", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Records", "IsRevenue");
        }
    }
}
