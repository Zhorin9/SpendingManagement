using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpendingManagement.WebUI.Models
{
    public class SortingInfo
    {
        public string DataSort { get; set; }
        public string NameSort { get; set; }
        public string ChargeSort { get; set; }
        public string CategorySort { get; set; }
        public string CurrentSearch { get; set; }
        public string CurrentSort { get; set; }
    }
}