using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using SpendingManagement.Domain.Abstract;
using SpendingManagement.Domain.Entities;
using SpendingManagement.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace SpendingManagement.WebUI.Controllers
{
    public class ExpenseController : Controller
    {
        private IExpenseRepository repository;
        private IUserRepository userRepository;
        public ExpenseController(IExpenseRepository expenseRepository, IUserRepository userRepository)
        {
            this.repository = expenseRepository;
            this.userRepository = userRepository;
        }
        public ViewResult Statistics(string dateFromParam, string dateToParam)
        {
            if (String.IsNullOrEmpty(dateFromParam)) { dateFromParam = "1900-01-01"; }
            if (String.IsNullOrEmpty(dateToParam)) { dateToParam = "2100-01-01"; }
            var repoParam = repository.Expenses.Where(p => _ConvertStringToDateTime(p.Date) >= _ConvertStringToDateTime(dateFromParam)
                && _ConvertStringToDateTime(p.Date) <= _ConvertStringToDateTime(dateToParam));
            StatisticsViewModel statistics = new StatisticsViewModel()
            {
                SumCharge = repoParam.Sum(p => p.Charge),
                CategoriesCharge = _SelectExtremeValues(repoParam),
            };

            string[] xValuesLineSeries = repoParam.Select(p => p.Date).Distinct().ToArray();                                    //create array with arguments to line function
            IEnumerable<decimal> yValuesLineSeries = repoParam.GroupBy(p => p.Date).Select(g => g.Sum(s => s.Charge));          //create list with values of the function
            statistics.CreateLineChart(xValuesLineSeries, yValuesLineSeries);

            statistics.CreatePieChart(_CreatePieSeries(repoParam));                            
            return View(statistics);
        }
        public ViewResult Index()
        {
            DateTimeFormatInfo info = new DateTimeFormatInfo();
            DashboardViewModel dashboard = new DashboardViewModel()
            {
                CurrentMonthName = new CultureInfo("pl").DateTimeFormat.GetMonthName(DateTime.Now.Month)
            };
            dashboard.SumYearCharge = repository.Expenses.Where(p=> _ConvertStringToDateTime(p.Date).Year == DateTime.Now.Year).Select(p => p.Charge).Sum();
            dashboard.SumMonthCharge = repository.Expenses.Where(p => _ConvertStringToDateTime(p.Date).Month == DateTime.Now.Month)
                .Select(p => p.Charge).Sum();
            dashboard.SumWeekCharge = repository.Expenses.Where(p => _ConvertStringToDateTime(p.Date) >= _GetFirstDayOfWeek().Date && _ConvertStringToDateTime(p.Date) <= DateTime.Now.Date)
            .Select(p => p.Charge).Sum();
            
            var lastTenExpenses = repository.Expenses.Reverse().OrderBy(p => p.Date).Reverse().Take(10);
            dashboard.Expsenses = lastTenExpenses;

            return View(dashboard);
        }
        private DateTime _ConvertStringToDateTime(string date)
        {
            return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        private DateTime _GetFirstDayOfWeek()
        {
            var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            DateTime currentDate = DateTime.Now;
            while (currentDate.DayOfWeek != firstDayOfWeek)
            {
                currentDate = currentDate.AddDays(-1);
            }
            return currentDate;
        }
        private List<object> _CreatePieSeries(IEnumerable<Expense> repoParam)
        {
            var category = repoParam.Select(p => p.Category).Distinct();
            List<object> series = new List<object>();
            category.ToList().ForEach(x => series.Add(new object[] { x, repoParam.
                Where(p => p.Category == x).
                Select(p => new { p.Category, p.Charge }).
                Sum(p => p.Charge) }));
            return series;
        }
        private List<object[]> _SelectExtremeValues(IEnumerable<Expense> repoParam)
        {
            List<object[]> CategoriesCharge = new List<object[]>();
            var categories = repoParam.Select(p => p.Category).Distinct();
            categories.ToList().ForEach(x => CategoriesCharge.Add(new object[] { x, repoParam.Where(p => p.Category == x).Select(p => p.Charge).Sum() }));
            return CategoriesCharge;
        }
    }
}