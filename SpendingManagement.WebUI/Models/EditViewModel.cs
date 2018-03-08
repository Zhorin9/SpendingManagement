using SpendingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpendingManagement.WebUI.Models
{
    public class EditViewModel
    {
        public IEnumerable<string> CategoryList { get; set; }
        public EditViewModel()
        {
            CategoryList = new List<string>()
            {
                "Jedzenie",
                "Ubrania",
                "Kosmetyki",
                "Alkohol",
                "Opłaty",
                "Przejazdy",
                "Inne",
            };
        }
        [HiddenInput(DisplayValue = false)]
        public int ExpenseID { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Proszę podać nazwę wydatku")]
        public string Name { get; set; }

        [Display(Name = "Opis"), DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Proszę podać opis wydatku")]
        public string Description { get; set; }

        [Display(Name = "Data")]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać właściwą wartość")]
        [Display(Name = "Koszt")]
        public decimal Charge { get; set; }

        [Required(ErrorMessage = "Proszę podać właściwą kategorię")]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
    }
}