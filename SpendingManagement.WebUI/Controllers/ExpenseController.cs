using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using SpendingManagement.Domain.Abstract;
using SpendingManagement.WebUI.Models;
using System;
using System.Collections.Generic;
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
        public ViewResult Statistics(string dateFrom, string dateTo)
        {
            Statistics statistics = new Statistics();
            statistics.CreatePieChart(repository);
            statistics.CreateLineChart(repository);
            return View(statistics);
        }

        public ActionResult Vhart()
        {
            Highcharts chart = new Highcharts("vhart").SetXAxis(new XAxis
            {
                Categories = new[] {
                    "Jan",
                    "Feb",
                    "Mar",
                    "Apr",
                    "May",
                    "Jun",
                    "Jul",
                    "Aug",
                    "Sep",
                    "Oct",
                    "Nov",
                    "Dec" }
            }).SetSeries(new Series
            {
                Data = new Data(new object[] {
                    29.9,
                    71.5,
                    106.4,
                    129.2,
                    144.0,
                    176.0,
                    135.6,
                    148.5,
                    216.4,
                    .1,
                    95.6,
                    54.4
                })
            });
            return PartialView("Vhart", chart);
        }
    }
}