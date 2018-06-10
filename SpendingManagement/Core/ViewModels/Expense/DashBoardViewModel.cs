using SpendingManagement.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SpendingManagement.Core.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<Record> LastTenExpenes = new List<Record>();
        public decimal SumYearCharge { get; set; }
        public decimal SumMonthCharge { get; set; }  
        public decimal SumWeekCharge { get; set; }

        public IEnumerable<Record> LastTenRevenues { get; set; }
        public decimal MonthRevenue { get; set; }
        public decimal YearRevenue { get; set; }
        public decimal WeekRevenue { get; set; }

        public string CurrentMonthName { get; set; }

        public DashboardViewModel()
        {
            CurrentMonthName = new CultureInfo("pl").DateTimeFormat.GetMonthName(DateTime.Now.Month);
        }

    }
}