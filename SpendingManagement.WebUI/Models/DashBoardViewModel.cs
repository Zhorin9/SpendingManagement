using SpendingManagement.Domain.Abstract;
using SpendingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpendingManagement.WebUI.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<Expense> Expsenses = new List<Expense>();
        public decimal SumCharge { get; set; }
        public decimal SumMonthCharge { get; set; }  
    }
}