using SpendingManagement.Domain.Entities;
using System.Collections.Generic;

namespace SpendingManagement.Domain.Abstract
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> Expenses { get; }
        void SaveExpense(Expense expense);
        Expense DeleteExpense(int expenseID);
    }
}
