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
using SpendingManagement.Domain.Entities;

namespace SpendingManagement.WebUI.Models
{
    public class StatisticsViewModel
    {
        public Highcharts PieChart { get; set; }
        public Highcharts LineSumChart { get; set; }
        public Highcharts LineCategoryChart { get; set; }

        public decimal SumCharge { get; set; }
        public List<object[]> CategoriesCharge = new List<object[]>();



        public void CreatePieChart(List<object> series)
        {
            PieChart = new Highcharts("CategoryChart").SetSeries(new Series
            {
                Type = ChartTypes.Pie,
                Data = new Data(series.ToArray()),
                Name = "Wydatki",
            }).SetTitle(new Title() { Text = "Udział poszczególnych kategorii" });
        }
        public void CreateLineChart(string[] xValuesParam, IEnumerable<decimal> yValuesParam)
        {
            List<object> series = new List<object>();
            foreach (var p in yValuesParam) { series.Add(p); }

            LineSumChart = new Highcharts("LineChart").SetSeries(new Series
            {
                Type = ChartTypes.Line,
                Data = new Data(series.ToArray()),
                Name = "Wydatki dzienne",
            }).SetTitle(new Title()
            {
                Text = "Wykres wydatków w danym dniu"
            }).SetXAxis(new XAxis()
            {
                Labels = new XAxisLabels() { Enabled = false, },
                Type = AxisTypes.Datetime,
                Title = new XAxisTitle() { Text = "Data" },
                Categories = xValuesParam,
            }).SetYAxis(new YAxis()
            {
                TickInterval = 50,
                Title = new YAxisTitle() { Text = "Wartość [zł]"},
                //Min = 0,
            }).SetLegend(new Legend()
            {
                Enabled = false,
            });
        }

        /*
        public void CreateCategoryLineChart(IEnumerable<Expense> repository)
        {
            List<object> foodCategory = new List<object>();
            List<object> gearCategory = new List<object>();
            List<object> cosmetics = new List<object>();


            var categories = repository.Select(p => p.Category).Distinct().ToList();
            var xValues = repository.Where(p=> p.Category == "Jedzenie")
                .Select(p => p.Date).Distinct().ToArray();
            foreach (var p in xValues) { foodCategory.Add(p); }



            var cat1 = repository.Where(p => categories[0] == p.Category).GroupBy(p => p.Date).Select(g => g.Sum(s => s.Charge) );
            List<object> series = new List<object>();
            foreach (var p in cat1) { series.Add(p); }

            var cat2 = repository.Where(p => categories[1] == p.Category).GroupBy(p => p.Date).Select(g => g.Sum(s => s.Charge));
            List<object> series2 = new List<object>();
            foreach (var p in cat2) { series2.Add(p); }

            LineCategoryChart = new Highcharts("Kat1").SetSeries(new []
            {
                 new Series{
                    Type = ChartTypes.Line,
                    Data = new Data(series.ToArray()),
                    Name = "Wydatki dzienne",
                },
                new Series{
                    Type = ChartTypes.Line,
                    Data = new Data(series2.ToArray()),
                    Name = "Wydatki ",
                }
            }).SetTitle(new Title()
            {
                Text = "Wykres poszczególnych kategorii"
            }).SetXAxis(new XAxis()
            {
                Labels = new XAxisLabels() { Enabled = false, },
                Type = AxisTypes.Datetime,
                Title = new XAxisTitle() { Text = "Data" },
                Categories = xValues,
            }).SetYAxis(new YAxis()
            {
                TickInterval = 50,
                Title = new YAxisTitle() { Text = "Wartość [zł]" },
                //Min = 0,
            }).SetLegend(new Legend()
            {
                Enabled = false,
            });
        }
        */
    }

}