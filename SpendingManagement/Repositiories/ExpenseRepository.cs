using SpendingManagement.Core.Models;
using SpendingManagement.Core.Repositiories;
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
        public Expense GetExpense(int expenseId)
        {
            return _contex.Expenses
               .SingleOrDefault(e => e.Id == expenseId);
        }
    }
}