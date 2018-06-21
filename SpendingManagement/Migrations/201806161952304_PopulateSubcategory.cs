namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSubcategory1 : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Subcategories ON");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (1, 'Bar', 1)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (2, 'Sklep', 1)");

            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (3, 'Inne', 2)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (4, 'Sprzêt', 2)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (5, 'Œrodki czystoœci', 2)");

            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (6, 'Elektronika', 3)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (7, 'Grawerowanie', 3)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (8, 'Inne', 3)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (9, 'Terrarystyka', 3)");

            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (10, 'Inne', 4)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (11, 'Ksi¹¿ka', 4)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (12, 'Wakacje', 4)");

            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (13, 'Bar', 5)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (14, 'Inne', 5)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (15, 'Kanapki', 5)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (16, 'Sklep', 5)");

            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (17, 'Inne', 6)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (18, 'Internet', 6)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (19, 'Mieszkanie', 6)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (20, 'Telefon', 6)");

            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (21, 'Fryzjer', 7)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (22, 'Inne', 7)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (23, 'Kosmetyki', 7)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (24, 'Prezent', 7)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (25, 'Ubranie', 7)");

            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (26, 'Inne', 8)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (27, 'Miesiêczny', 8)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (28, 'Powrót do domu', 8)");

            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (29, 'Inne', 9)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (30, 'Kino', 9)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (31, 'Koncerty', 9)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (32, 'Sporty', 9)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (33, 'StandUp', 9)");

            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (34, 'Inne', 10)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (35, 'Praca', 10)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (36, 'Przelew', 10)");
            Sql("INSERT INTO Subcategories (Id, Name, CategoryId) VALUES (37, 'Wygrana', 10)");
            Sql("SET IDENTITY_INSERT Subcategories OFF");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Subcategories WHERE Id BETWEEN 1 AND 37");
        }
    }
}
