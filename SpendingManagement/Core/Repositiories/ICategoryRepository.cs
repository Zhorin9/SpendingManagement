
using SpendingManagement.Core.Models;
using System.Collections.Generic;

namespace SpendingManagement.Core.Repositiories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Return all references to category entity
        /// </summary>
        IEnumerable<Categories> GetCategories();

        /// <summary>
        /// Return prepared dictionary with categories and subcategories
        /// </summary>
        Dictionary<string, List<string>> GetCategoriesDictionary(bool isRevenue);
    }
}
