using SpendingManagement.Core.Models;
using SpendingManagement.Core.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpendingManagement.Repositiories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly ApplicationDbContext _contex = new ApplicationDbContext();

        public ExpenseRepository(ApplicationDbContext contex)
        {
            _contex = contex;
        }

        public IEnumerable<Expense> Expenses { get { return _contex.Expenses; } }

        public void AddExpense(Expense expense)
        {
            _contex.Expenses.Add(expense);
        }

        public void Complete()
        {
            _contex.SaveChanges();
        }

        public void DeleteExpense(Expense expense)
        {
            _contex.Expenses.Remove(expense);
        }

        public Expense GetExpense(string userId, int expenseId)
        {
            return _contex.Expenses
                .SingleOrDefault(e => e.Id == expenseId && e.UserID == userId);
        }

        public IEnumerable<Expense> GetExpenses(string userId, int amountOfExpenses)
        {
            return _contex.Expenses.Where(u=> u.UserID == userId)
                .OrderBy(o => o.Date)
                .Take(amountOfExpenses);
        }

        public IEnumerable<Expense> GetExpensesInSelectedRange(DateTime? dateFrom, DateTime? dateTo)
        {
            return _contex.Expenses
                .Where(d => d.Date > dateFrom && d.Date < dateTo)
                .ToList();
        }
    }
}