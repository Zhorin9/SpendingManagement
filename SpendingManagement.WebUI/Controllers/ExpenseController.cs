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
        public ExpenseController(IExpenseRepository expenseRepository)
        {
            this.repository = expenseRepository;
        }
        public ViewResult Statistics(string dateFromParam, string dateToParam)
        {
            if (String.IsNullOrEmpty(dateFromParam)) { dateFromParam = "1900-01-01"; }
            if (String.IsNullOrEmpty(dateToParam)) { dateToParam = "2100-01-01"; }
            var repoParam = repository.Expenses.Where(p => ConvertStringToDateTime(p.Date) >= ConvertStringToDateTime(dateFromParam)
                && ConvertStringToDateTime(p.Date) <= ConvertStringToDateTime(dateToParam));

            StatisticsViewModel statistics = new StatisticsViewModel()
            {
                SumCharge = repoParam.Sum(p => p.Charge),
                CategoriesCharge = SelectExtremeValues(repoParam),
        };

            string[] xValuesLineSeries = repoParam.Select(p => p.Date).Distinct().ToArray();                                    //create array with arguments to line function
            IEnumerable<decimal> yValuesLineSeries = repoParam.GroupBy(p => p.Date).Select(g => g.Sum(s => s.Charge));          //create list with values of the function
            statistics.CreateLineChart(xValuesLineSeries, yValuesLineSeries);

            statistics.CreatePieChart(createPieSeries(repoParam));                            
            return View(statistics);
        }
        public ViewResult Index()
        {
            DashboardViewModel dashboard = new DashboardViewModel();
            dashboard.SumCharge = repository.Expenses.Select(p => p.Charge).Sum();
            dashboard.SumMonthCharge = repository.Expenses.Where(p => ConvertStringToDateTime(p.Date).Month == DateTime.Now.Month).Select(p => p.Charge).Sum();

            var lastTenExpenses = repository.Expenses.Reverse().OrderBy(p => p.Date).Reverse().Take(10);
            dashboard.Expsenses = lastTenExpenses;

            return View(dashboard);
        }
        private DateTime ConvertStringToDateTime(string date)
        {
            return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
        private List<object> createPieSeries(IEnumerable<Expense> repoParam)
        {
            var category = repoParam.Select(p => p.Category).Distinct();
            List<object> series = new List<object>();
            category.ToList().ForEach(x => series.Add(new object[] { x, repoParam.
                Where(p => p.Category == x).
                Select(p => new { p.Category, p.Charge }).
                Sum(p => p.Charge) }));
            return series;
        }
        private List<object[]> SelectExtremeValues(IEnumerable<Expense> repoParam)
        {
            List<object[]> CategoriesCharge = new List<object[]>();
            var categories = repoParam.Select(p => p.Category).Distinct();
            categories.ToList().ForEach(x => CategoriesCharge.Add(new object[] { x, repoParam.Where(p => p.Category == x).Select(p => p.Charge).Sum() }));
            return CategoriesCharge;
        } 
        
    }
}