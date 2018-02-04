using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using SpendingManagement.Domain.Abstract;
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
            StatisticsViewModel statistics = new StatisticsViewModel();
            var repoParam = repository.Expenses.Where(p => ConvertStringToDateTime(p.Date) >= ConvertStringToDateTime(dateFromParam)
                    && ConvertStringToDateTime(p.Date) <= ConvertStringToDateTime(dateToParam));
            statistics.ExtremeValues(repoParam);
            statistics.CreatePieChart(repoParam);
            statistics.CreateLineChart(repoParam);
            //statistics.CreateCategoryLineChart(repository);
            return View(statistics);
        }
        public ViewResult ReposiotoryParameter()
        {
            return View();
        }
        public ViewResult Index()
        {
            return View();
        }
        private DateTime ConvertStringToDateTime(string date)
        {
            return DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}