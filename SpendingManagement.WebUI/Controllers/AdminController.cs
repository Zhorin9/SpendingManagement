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
        [HttpPost]
        public ActionResult Edit(Expense expense)
        {
            if (ModelState.IsValid)
            {
                repository.SaveExpense(expense);
                TempData["message"] = string.Format("Zapisano {0} ", expense.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(expense);
            }
        } 
        public ViewResult Create()
        {
            return View("Edit", new Expense());
        }
    }
}