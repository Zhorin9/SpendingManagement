using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SpendingManagement.Domain.Entities
{
    public class Expense
    {
        public int ExpenseID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public decimal Charge { get; set; }
        public string Category { get; set; }
    }
}
