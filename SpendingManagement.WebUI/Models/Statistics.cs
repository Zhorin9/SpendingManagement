using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using SpendingManagement.Domain.Abstract;
using System.Globalization;

namespace SpendingManagement.WebUI.Models
{
    public class Statistics
    {
        public Highcharts PieChart { get; set; }
        public Highcharts LineChart { get; set; }
        private string dateFormat = "yyyy-MM-dd";
        public void CreatePieChart(IExpenseRepository repository)
        {
            var category = repository.Expenses.Select(p => p.Category).Distinct();
            List<object> series = new List<object>();
            category.ToList().ForEach(x => series.Add(new object[] { x, repository.Expenses.
                Where(p => p.Category == x).
                Select(p => new { p.Category, p.Charge }).
                Sum(p => p.Charge) }));

            PieChart = new Highcharts("CategoryChart").SetSeries(new Series
            {
                Type = ChartTypes.Pie,
                Data = new Data(series.ToArray()),
            }).SetTitle(new Title() { Text = "Udział poszczególnych kategorii" });
        }
        public void CreateLineChart(IExpenseRepository repository)
        {
            //var axis = repository.Expenses.Select(p => Convert.ToDateTime(p.Date)).ToArray();
            //List<object> series = new List<object>();
            var series = repository.Expenses.Select(p =>  new { X = DateTime.ParseExact(p.Date, dateFormat , CultureInfo.InvariantCulture), p.Charge }).ToArray();

            /*
            List<>

            
            LineChart = new Highcharts("LineChart")
                .SetSeries(new Series
            {
                Type = ChartTypes.Line,
                Data = new Data(series)
            });
            */
        }
    }
}