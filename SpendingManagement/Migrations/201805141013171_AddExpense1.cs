namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpense1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Expenses", new[] { "User_Id" });
            DropColumn("dbo.Expenses", "UserID");
            RenameColumn(table: "dbo.Expenses", name: "User_Id", newName: "UserID");
            AlterColumn("dbo.Expenses", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Expenses", "UserID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Expenses", new[] { "UserID" });
            AlterColumn("dbo.Expenses", "UserID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Expenses", name: "UserID", newName: "User_Id");
            AddColumn("dbo.Expenses", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Expenses", "User_Id");
        }
    }
}
