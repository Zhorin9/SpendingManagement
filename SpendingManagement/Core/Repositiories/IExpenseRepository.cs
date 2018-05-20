using SpendingManagement.Core.Models;
using System;
using System.Collections.Generic;

namespace SpendingManagement.Core.Repositiories
{
    public interface IExpenseRepository
    {
        IEnumerable<Expense> Expenses { get; }
        void AddExpense(Expense expense);
        void DeleteExpense(Expense expense);
        Expense GetExpense(string userId, int expenseId);
        IEnumerable<Expense> GetExpensesInSelectedRange(DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<Expense> GetExpenses(string userId, int amountOfExpenses);
        void Complete();
    }
}
