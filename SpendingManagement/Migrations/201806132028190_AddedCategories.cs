namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Category = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subcategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Categories_Id = c.Byte(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Categories_Id)
                .Index(t => t.Categories_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Subcategories", "Categories_Id", "dbo.Categories");
            DropIndex("dbo.Subcategories", new[] { "Categories_Id" });
            DropTable("dbo.Subcategories");
            DropTable("dbo.Categories");
        }
    }
}
