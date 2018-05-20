using SpendingManagement.Controllers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SpendingManagement.Core.ViewModels
{
    public class ExpenseFormViewModel
    {
        public string Heading { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Proszę podać nazwę wydatku")]
        public string Name { get; set; }

        [Display(Name = "Opis"), DataType(DataType.MultilineText)]
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

        public string Action
        {
            get
            {
                Expression<Func<ExpensesController, ActionResult>> update =
                    (c => c.Update(this));
                Expression<Func<ExpensesController, ActionResult>> create =
                    (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;

            }
        }
    }
}