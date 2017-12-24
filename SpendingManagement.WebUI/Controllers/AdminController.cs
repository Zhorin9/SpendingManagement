using SpendingManagement.Domain.Abstract;
using SpendingManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpendingManagement.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IExpenseRepository repository;
        public AdminController(IExpenseRepository expenseRepository)
        {
            repository = expenseRepository;
        }
        public ViewResult Index()
        {
            return View(repository.Expenses);
        }
        public ViewResult Edit(int expenseParam)
        {
            Expense expense = repository.Expenses.First(p => p.ExpenseID == expenseParam);
            return View(expense);
        }
    }
}