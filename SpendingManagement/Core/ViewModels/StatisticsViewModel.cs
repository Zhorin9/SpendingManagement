using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Collections.Generic;

namespace SpendingManagement.Core.ViewModels
{
    public class StatisticsViewModel
    {
        public decimal SumCharge { get; set; }
        public List<object[]> CategoriesCharge = new List<object[]>();
    }
}