using SpendingManagement.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpendingManagement.WebUI.Controllers
{
    public class ExpenseController : Controller
    {
        private IExpenseRepository repository;
        public ExpenseController(IExpenseRepository expenseRepository)
        {
            this.repository = expenseRepository;
        }
        public ViewResult List()
        {
            return View(repository.Expenses);
        }
    }
}