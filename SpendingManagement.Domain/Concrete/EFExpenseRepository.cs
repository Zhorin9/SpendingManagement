using SpendingManagement.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpendingManagement.Domain.Entities;
namespace SpendingManagement.Domain.Concrete
{
    public class EFExpenseRepository : IExpenseRepository
    {
        private EFDbContex contex = new EFDbContex();
        public IEnumerable<Expense> Expenses
        {
            get { return contex.Expenses; }
        }

        public Expense DeleteExpense(int expenseID)
        {
            Expense dbEntry = contex.Expenses.Find(expenseID);
            if (dbEntry != null) {
                contex.Expenses.Remove(dbEntry);
                contex.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveExpense(Expense expense)
        {
            if (expense.ExpenseID == 0) {
                contex.Expenses.Add(expense);
            }


            else {
                Expense dbEntry = contex.Expenses.Find(expense.ExpenseID);
                if (dbEntry != null)
                {
                    dbEntry.Name = expense.Name;
                    dbEntry.Description = expense.Description;
                    dbEntry.Date = expense.Date;
                    dbEntry.Charge = expense.Charge;
                    dbEntry.Category = expense.Category;
                    dbEntry.UserID = expense.UserID;
                }
            }
            contex.SaveChanges();
        }
    }
}
