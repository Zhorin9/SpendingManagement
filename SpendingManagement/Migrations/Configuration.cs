using System.Collections.Generic;
using SpendingManagement.Core.Models;

namespace SpendingManagement.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SpendingManagement.Core.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SpendingManagement.Core.Models.ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(x => x.Id,
                new Categories()
                {
                    Id = 1,
                    IsRevenue = false,
                    Name = "Alkohol",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 1, Name = "Bar"},
                        new Subcategories(){ Id = 2, Name = "Sklep"},
                    }
                },
                new Categories()
                {
                    Id = 2,
                    IsRevenue = false,
                    Name = "Dom",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 3, Name = "Inne"},
                        new Subcategories(){ Id = 4, Name = "Sprzêt"},
                        new Subcategories(){ Id = 5, Name = "Œrodki czystoœci"},
                    }
                },
                new Categories()
                {
                    Id = 3,
                    IsRevenue = false,
                    Name = "Hobby",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 6, Name = "Elektronika"},
                        new Subcategories(){ Id = 7, Name = "Grawerowanie"},
                        new Subcategories(){ Id = 8, Name = "Inne"},
                        new Subcategories(){ Id = 9, Name = "Terrarystyka"},
                    }
                },
                new Categories()
                {
                    Id = 4,
                    IsRevenue = false,
                    Name = "Inne",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 10, Name = "Inne"},
                        new Subcategories(){ Id = 11, Name = "Ksi¹¿ka"},
                        new Subcategories(){ Id = 12, Name = "Ksi¹¿ka"},
                    }
                },
                new Categories()
                {
                    Id = 5,
                    IsRevenue = false,
                    Name = "Jedzenie",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 13, Name = "Bar"},
                        new Subcategories(){ Id = 14, Name = "Inne"},
                        new Subcategories(){ Id = 15, Name = "Kanapki"},
                        new Subcategories(){ Id = 16, Name = "Sklep"},
                    }
                },
                new Categories()
                {
                    Id = 6,
                    IsRevenue = false,
                    Name = "Op³aty",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 17, Name = "Inne"},
                        new Subcategories(){ Id = 18, Name = "Internet"},
                        new Subcategories(){ Id = 19, Name = "Mieszkanie"},
                        new Subcategories(){ Id = 20, Name = "Telefon"},
                    }
                },
                new Categories()
                {
                    Id = 7,
                    IsRevenue = false,
                    Name = "Osobiste",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 21, Name = "Fryzjer"},
                        new Subcategories(){ Id = 22, Name = "Inne"},
                        new Subcategories(){ Id = 23, Name = "Kosmetyki"},
                        new Subcategories(){ Id = 24, Name = "Prezent"},
                        new Subcategories(){ Id = 25, Name = "Ubranie"},
                    }
                },
                new Categories()
                {
                    Id = 8,
                    IsRevenue = false,
                    Name = "Przejazdy",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 26, Name = "Inne"},
                        new Subcategories(){ Id = 27, Name = "Miesiêczny"},
                        new Subcategories(){ Id = 28, Name = "Powrót do domu"},
                    }
                },
                new Categories()
                {
                    Id = 9,
                    IsRevenue = false,
                    Name = "Rozrywka",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 29, Name = "Inne"},
                        new Subcategories(){ Id = 30, Name = "Kino"},
                        new Subcategories(){ Id = 31, Name = "Koncerty"},
                        new Subcategories(){ Id = 32, Name = "Sporty"},
                        new Subcategories(){ Id = 33, Name = "StandUp"},
                    }
                },
                new Categories()
                {
                    Id = 10,
                    IsRevenue = true,
                    Name = "Przychód",
                    Subcategories = new List<Subcategories>()
                    {
                        new Subcategories(){ Id = 34, Name = "Inne"},
                        new Subcategories(){ Id = 35, Name = "Praca"},
                        new Subcategories(){ Id = 36, Name = "Przelew"},
                        new Subcategories(){ Id = 37, Name = "Wygrana"},
                    }
                });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
