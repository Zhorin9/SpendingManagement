using SpendingManagement.Controllers;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SpendingManagement.Core.Models
{
    public class Record
    {
        public int Id { get; set; }

        public bool IsRevenue { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public decimal Charge { get; set; }

        public string Category { get; set; }

        public string Subcategory { get; set; }

        public ApplicationUser User { get; set; }

        public string UserID { get; set; }
    }
}
