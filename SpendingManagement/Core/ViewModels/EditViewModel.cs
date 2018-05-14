using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SpendingManagement.Core.ViewModels
{
    public class EditViewModel
    {
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

        [Required(ErrorMessage = "Proszę wybrać kategorię")]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Proszę wybrać podkategorię")]
        [Display(Name = "Podkategoria")]
        public string Subcategory { get; set; }
    }
}