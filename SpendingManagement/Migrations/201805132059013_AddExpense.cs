namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpense : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Charge = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Category = c.String(),
                        Subcategory = c.String(),
                        UserID = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Expenses", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Expenses", new[] { "User_Id" });
            DropTable("dbo.Expenses");
        }
    }
}
