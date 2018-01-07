using SpendingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpendingManagement.WebUI.Models
{
    public class ExpensesListViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public SortingInfo SortingInfo { get; set; }
    }
}