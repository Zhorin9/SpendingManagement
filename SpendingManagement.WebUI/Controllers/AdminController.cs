using SpendingManagement.Domain.Abstract;
using SpendingManagement.Domain.Entities;
using SpendingManagement.WebUI.Models;
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
        private int PageSize = 8;
        public AdminController(IExpenseRepository expenseRepository)
        {
            repository = expenseRepository;
        }
        public ViewResult Index(SortingInfo sortingInfo, string sortOrder, string searchString, int page = 1)
        {
            
            ViewBag.DataSortParam = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.ChargeSortParam = sortOrder == "charge" ? "charge_desc" : "charge";
            ViewBag.CategorySortParam = sortOrder == "category" ? "category_desc" : "category";

            var parameters = repository.Expenses;

            if (!String.IsNullOrEmpty(searchString))
            {
                parameters = parameters.Where(s => s.Name.Contains(searchString) || s.Category.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "date_desc":
                   parameters = parameters.OrderByDescending(s => s.Date);
                    break;

                case "name":
                    parameters = parameters.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    parameters = parameters.OrderByDescending(s => s.Name);
                    break;

                case "category":
                    parameters = parameters.OrderBy(s => s.Category);
                    break;
                case "category_desc":
                    parameters = parameters.OrderByDescending(s => s.Category);
                    break;

                case "charge":
                    parameters = parameters.OrderBy(s => s.Charge);
                    break;
                case "charge_desc":
                    parameters = parameters.OrderByDescending(s => s.Charge);
                    break;

                default:
                    parameters = parameters.OrderBy(s => s.Date);
                    break;
            }

            ExpensesListViewModel model = new ExpensesListViewModel
            {
                Expenses = parameters.Skip((page - 1) * PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = parameters.Count(),
                },
                SortingInfo = new SortingInfo
                {
                    CurrentSearch = searchString,
                    CurrentSort = sortOrder,
                }
               
            };
            return View(model);
        }
        public ViewResult Edit(int id)
        {
            Expense expense = repository.Expenses.First(p => p.ExpenseID == id);
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
        public ViewResult Details(int id)
        {
            Expense expense = repository.Expenses.First(p=> p.ExpenseID == id);
            return View(expense);
        }
        public ViewResult Create()
        {
            return View("Edit", new Expense());
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Expense deleteExpense = repository.DeleteExpense(id);
            if(deleteExpense != null)
            {
                TempData["message"] = string.Format("Usunięto {0}", deleteExpense.Name);
            }
            return RedirectToAction("Index");

        }
    }
}