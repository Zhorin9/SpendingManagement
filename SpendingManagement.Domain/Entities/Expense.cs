using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SpendingManagement.Domain.Entities
{
    public class Expense
    {
        [HiddenInput(DisplayValue =false)]
        public int ExpenseID { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Opis"), DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name = "Data")]
        public string Date { get; set; }
        [Display(Name = "Koszt")]
        public decimal Charge { get; set; }
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
    }
}
