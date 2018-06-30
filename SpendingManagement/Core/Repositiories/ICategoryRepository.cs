
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

        /// <summary>
        /// Return prepared list with all categoriess
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetCategoriesList();

        /// <summary>
        /// Return prepared subcategories list for selected category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        List<string> GetSubcategoriesList(string category);
    }
}
