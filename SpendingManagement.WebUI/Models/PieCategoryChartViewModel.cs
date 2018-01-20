using SpendingManagement.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts;
using System.Web.Helpers;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;

namespace SpendingManagement.WebUI.Models
{
    public class PieCategoryChartViewModel
    {
        private IExpenseRepository repository;
        public PieCategoryChartViewModel(IExpenseRepository repositoryParam)
        {
            repository = repositoryParam;
        }
        public Highcharts CreatePieChart()
        {
            var xAxis = repository.Expenses.Select(x => x.Category).ToArray();
            var series = repository.Expenses.Select(x => new object[] { x.Category }).ToArray();
            Highcharts pieChart = new Highcharts("CategoryExpense")
            .SetTitle(new Title { Text = "Udział poszczególnych kategorii" })
            .SetXAxis(new XAxis { Categories = xAxis })
            .SetSeries(new[]
            {
                new Series{ Name = "", Data = new Data(series)}
            });
            return pieChart;
        }
    }
}