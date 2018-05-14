using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Collections.Generic;

namespace SpendingManagement.Core.ViewModels
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
    }

}