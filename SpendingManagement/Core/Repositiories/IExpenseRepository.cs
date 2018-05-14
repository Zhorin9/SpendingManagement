using SpendingManagement.Core.Models;
using System.Collections.Generic;

namespace SpendingManagement.Core.Repositiories
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> Expenses { get; }
        void AddExpense(Expense expense);
    }
}
