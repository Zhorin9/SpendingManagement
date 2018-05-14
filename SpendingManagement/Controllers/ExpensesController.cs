using Microsoft.AspNet.Identity;
using SpendingManagement.Core.Models;
using SpendingManagement.Core.Repositiories;
using SpendingManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;


namespace SpendingManagement.Controllers
{
    [Authorize]
    public class ExpensesController : Controller
    {
        private readonly IExpenseRepository _expensesRepository;
        private readonly IApplicationUserRepository _usersRepository;

        private int PageSize = 8;
        public ExpensesController(IExpenseRepository expenseRepository, IApplicationUserRepository userRepository)
        {
            _expensesRepository = expenseRepository;
            _usersRepository = userRepository;
        }
        public ViewResult Index(SortingInfo sortingInfo, string sortOrder, string searchString, int page = 1)
        {
            var userId = User.Identity.GetUserId();

            sortingInfo.DataSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            sortingInfo.NameSort = sortOrder == "name" ? "name_desc" : "name";
            sortingInfo.ChargeSort = sortOrder == "charge" ? "charge_desc" : "charge";
            sortingInfo.CategorySort = sortOrder == "category" ? "category_desc" : "category";
            sortingInfo.SubcategorySort = sortOrder == "subcategory" ? "subcategory_desc" : "subcategory";
            var parameters = _expensesRepository.Expenses.Where(p => p.UserID == userId);
 
            if (!String.IsNullOrEmpty(searchString))
            {
                parameters = parameters.Where(p => p.Name.Contains(searchString) || p.Category.Contains(searchString) && p.UserID == userId);
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
                case "subcategory":
                    parameters = parameters.OrderBy(s => s.Subcategory);
                    break;
                case "subcategory_desc":
                    parameters = parameters.OrderByDescending(s => s.Subcategory);
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
                    NameSort = sortingInfo.NameSort,
                    CategorySort = sortingInfo.CategorySort,
                    SubcategorySort = sortingInfo.SubcategorySort,
                    DataSort = sortingInfo.DataSort,
                    ChargeSort = sortingInfo.ChargeSort,
                }
               
            };
            return View(model);
        }
        public ViewResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            Expense expense = _expensesRepository.Expenses.First(p => p.Id == id && p.UserID == userId);
            EditViewModel model = new EditViewModel()
            { 
                ExpenseID = expense.Id,
                Name = expense.Name,
                Charge = expense.Charge,
                Category = expense.Category,
                Date = expense.Date,
                Description = expense.Description,
                Subcategory = expense.Subcategory,
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            var userId = User.Identity.GetUserId();
            Expense expense;
            if(model.ExpenseID == 0)
            {
                expense = new Expense();
            }
            else
            {
                expense = _expensesRepository.Expenses.First(p => p.Id == model.ExpenseID);
            }
            
            expense.Name = model.Name;
            expense.Charge = model.Charge;
            expense.Category = model.Category;
            expense.Date = model.Date;
            expense.Description = model.Description;
            expense.Subcategory = model.Subcategory;
            expense.UserID = userId;

            if (ModelState.IsValid)
            {
                _expensesRepository.AddExpense(expense);
                TempData["message"] = string.Format("Zapisano {0} ", expense.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        public ViewResult Statistics(DateTime? dateFromParam, DateTime? dateToParam)
        {
            var userId = User.Identity.GetUserId();

            if (dateFromParam == null) { dateFromParam = DateTime.Parse("1900-01-01"); }
            if (dateToParam == null) { dateToParam = DateTime.Parse("2100-01-01"); }
            var repoParam = _expensesRepository.Expenses.Where(p => p.Date >= dateFromParam
                && p.Date <= dateToParam && p.UserID == userId);
            StatisticsViewModel statistics = new StatisticsViewModel()
            {
                SumCharge = repoParam.Sum(p => p.Charge),
                CategoriesCharge = _SelectExtremeValues(repoParam),
            };

            string[] xValuesLineSeries = repoParam.Select(p => p.Date.ToShortDateString()).Distinct().ToArray();                                    //create array with arguments to line function
            IEnumerable<decimal> yValuesLineSeries = repoParam.GroupBy(p => p.Date).Select(g => g.Sum(s => s.Charge));          //create list with values of the function
            statistics.CreateLineChart(xValuesLineSeries, yValuesLineSeries);

            statistics.CreatePieChart(_CreatePieSeries(repoParam));
            return View(statistics);
        }


        public ViewResult Details(int id)
        {
            Expense expense = _expensesRepository.Expenses.First(p=> p.Id == id);
            return View(expense);
        }
        public ViewResult Create()
        {
            return View("Edit", new EditViewModel());
        }
        /*
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
        */
        private List<object> _CreatePieSeries(IEnumerable<Expense> repoParam)
        {
            var category = repoParam.Select(p => p.Category).Distinct();
            List<object> series = new List<object>();
            category.ToList().ForEach(x => series.Add(new object[] { x, repoParam.
                Where(p => p.Category == x).
                Select(p => new { p.Category, p.Charge }).
                Sum(p => p.Charge) }));
            return series;
        }
        private List<object[]> _SelectExtremeValues(IEnumerable<Expense> repoParam)
        {
            List<object[]> CategoriesCharge = new List<object[]>();
            var categories = repoParam.Select(p => p.Category).Distinct();
            categories.ToList().ForEach(x => CategoriesCharge.Add(new object[] { x, repoParam.Where(p => p.Category == x).Select(p => p.Charge).Sum() }));
            return CategoriesCharge;
        }
    }
}