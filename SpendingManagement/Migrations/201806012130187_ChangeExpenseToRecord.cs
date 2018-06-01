namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeExpenseToRecord : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Expenses", newName: "Records");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Records", newName: "Expenses");
        }
    }
}
