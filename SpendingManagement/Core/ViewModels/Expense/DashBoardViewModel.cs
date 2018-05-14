using SpendingManagement.Core.Models;
using System.Collections.Generic;

namespace SpendingManagement.Core.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Expense> Expsenses = new List<Expense>();
        public decimal SumYearCharge { get; set; }
        public decimal SumMonthCharge { get; set; }  
        public decimal SumWeekCharge { get; set; }
        public string CurrentMonthName { get; set; }
    }
}