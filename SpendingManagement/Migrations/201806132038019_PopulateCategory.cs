namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Categories", "Category");

            Sql("INSERT INTO Categories (Id, Name) VALUES (1, 'Alkohol')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (2, 'Dom')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (3, 'Hobby')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (4, 'Inne')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (5, 'Jedzenie')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (6, 'Op³aty')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (7, 'Osobiste')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (8, 'Przejazdy')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (9, 'Rozrywka')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (10, 'Przychód')");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Category", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Categories", "Name");

            Sql("DELETE FROM Categories WHERE Id BETWEEN 1 AND 10");
        }
    }
}
