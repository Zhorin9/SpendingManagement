using SpendingManagement.Core.Models;
using SpendingManagement.Core.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpendingManagement.Repositiories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _contex = new ApplicationDbContext();

        public CategoryRepository(ApplicationDbContext contex)
        {
            _contex = contex;
        }

        public IEnumerable<Categories> GetCategories()
        {
            return _contex.Categories;
        }

        public Dictionary<string, List<string>> GetCategoriesDictionary(bool isRevenue)
        {
            return _contex.Categories.Where(p=> p.isRevenue == isRevenue)
                .Select(p => new { p.Name, p.Subcategories })
                .ToDictionary(p => p.Name, p => p.Subcategories.Select(n => n.Name).ToList());
        }

        public IEnumerable<string> GetCategoriesList(bool isRevenue)
        {
            return _contex.Categories
                .Where(p=> p.isRevenue == isRevenue)
                .Select(p => p.Name)
                .ToList();
        }

        public IEnumerable<string> GetSubcategoriesList(string category)
        {
            var subcategoriesList = _contex.Categories
                .Where(p => p.Name == category)
                .Select(p => p.Subcategories)
                .FirstOrDefault();
            if (subcategoriesList != null)
                return subcategoriesList.Select(g => g.Name).ToList();
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}