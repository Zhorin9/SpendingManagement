using SpendingManagement.Core.Models;
using System.Collections.Generic;

namespace SpendingManagement.Core.ViewModels
{
    public class ExpensesListViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public SortingInfo SortingInfo { get; set; }
    }
}