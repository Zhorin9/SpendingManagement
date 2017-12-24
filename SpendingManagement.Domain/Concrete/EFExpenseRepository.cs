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
    }
}
