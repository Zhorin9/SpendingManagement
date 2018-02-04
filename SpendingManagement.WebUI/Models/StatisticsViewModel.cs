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
        public decimal FoodSum { get; set; }
        public decimal CosmeticsSum { get; set; }
        public decimal AlcoholSum { get; set; }
        public decimal FeeSum { get; set; }
        public decimal RideSum { get; set; }
        public decimal AnotherChargeSum { get; set; }
        public decimal ClothingSum { get; set; }

        private List<string> CategoryList = new List<string>()
            {
                "Alkohol",
                "Inne",
                "Jedzenie",
                "Kosmetyki",
                "Opłaty",
                "Przejazdy",
                "Ubranie",                
            };
        public void ExtremeValues(IEnumerable<Expense> repository)
        {
           // List<string> categories = repository.OrderBy(p=>p.Category).Select(p => p.Category).Distinct().ToList();
            SumCharge = repository.Select(p => p.Charge).Sum();
            AlcoholSum = repository.Where(p => p.Category == CategoryList[0]).Select(p => p.Charge).Sum();
            AnotherChargeSum = repository.Where(p => p.Category == CategoryList[1]).Select(p => p.Charge).Sum();
            FoodSum = repository.Where(p => p.Category == CategoryList[2]).Select(p => p.Charge).Sum();
            CosmeticsSum = repository.Where(p => p.Category == CategoryList[3]).Select(p => p.Charge).Sum();
            FeeSum = repository.Where(p => p.Category ==  CategoryList[4]).Select(p => p.Charge).Sum();
            RideSum = repository.Where(p => p.Category == CategoryList[5]).Select(p => p.Charge).Sum();
            ClothingSum = repository.Where(p => p.Category == CategoryList[6]).Select(p => p.Charge).Sum();
        }

        public void CreatePieChart(IEnumerable<Expense> repository)
        {
            var category = repository.Select(p => p.Category).Distinct();
            List<object> series = new List<object>();
            category.ToList().ForEach(x => series.Add(new object[] { x, repository.
                Where(p => p.Category == x).
                Select(p => new { p.Category, p.Charge }).
                Sum(p => p.Charge) }));

            PieChart = new Highcharts("CategoryChart").SetSeries(new Series
            {
                Type = ChartTypes.Pie,
                Data = new Data(series.ToArray()),
                Name = "Wydatki",
            }).SetTitle(new Title() { Text = "Udział poszczególnych kategorii" });
        }
        public void CreateLineChart(IEnumerable<Expense> repository)
        {
            var xValues = repository.Select(p => p.Date).Distinct().ToArray();
            var yValues = repository.GroupBy(p => p.Date).Select(g=> g.Sum(s => s.Charge));
            List<object> series = new List<object>();
            foreach (var p in yValues) { series.Add(p); }

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
                Categories = xValues,
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
    }
}