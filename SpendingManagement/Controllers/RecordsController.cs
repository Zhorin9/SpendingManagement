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
    public class RecordsController : Controller
    {
        private readonly IRecordRepository _recordsRepository;
        private readonly IApplicationUserRepository _usersRepository;

        private int PageSize = 8;

        public RecordsController(IRecordRepository expenseRepository, IApplicationUserRepository userRepository)
        {
            _recordsRepository = expenseRepository;
            _usersRepository = userRepository;
        }

        public ViewResult RecordsList(SortingInfo sortingInfo, string sortOrder, string searchString, int page = 1)
        {
            var userId = User.Identity.GetUserId();

            sortingInfo.DataSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            sortingInfo.NameSort = sortOrder == "name" ? "name_desc" : "name";
            sortingInfo.ChargeSort = sortOrder == "charge" ? "charge_desc" : "charge";
            sortingInfo.CategorySort = sortOrder == "category" ? "category_desc" : "category";
            sortingInfo.SubcategorySort = sortOrder == "subcategory" ? "subcategory_desc" : "subcategory";
            var parameters = _recordsRepository.Records.Where(p => p.UserID == userId);
 
            if (!String.IsNullOrEmpty(searchString))
            {
                parameters = parameters.Where(p => p.Name.Contains(searchString) 
                        || p.Category.Contains(searchString) 
                        && p.UserID == userId);
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

            RecordsListViewModel model = new RecordsListViewModel
            {
                Records = parameters.Skip((page - 1) * PageSize).Take(PageSize),
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

        public ViewResult Create()
        {
            var form = new RecordFormViewModel()
            {
                Heading = "Stwórz wydatek"
            };
            return View("ExpenseForm", form);
        }

        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var expense = _recordsRepository.GetRecord(userId, id);

            if (expense == null)
                return HttpNotFound();

            //Expense expense = _expensesRepository.Expenses.First(p => p.Id == id && p.UserID == userId);
            RecordFormViewModel model = new RecordFormViewModel()
            {
                Heading = "Edycja - " + expense.Name,
                Id = expense.Id,
                Name = expense.Name,
                Charge = expense.Charge,
                Category = expense.Category,
                Date = expense.Date,
                Description = expense.Description,
                Subcategory = expense.Subcategory,
            };
            return View("ExpenseForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecordFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ExpenseForm", model);
            }

            var expense = new Record
            {
                UserID = User.Identity.GetUserId(),
                Date = model.Date,
                Charge = model.Charge,
                Category = model.Category,
                Subcategory = model.Subcategory,
                Name = model.Name
            };

            _recordsRepository.AddExpense(expense);
            _recordsRepository.Complete();

            TempData["message"] = string.Format("Zapisano {0} ", expense.Name);
            return RedirectToAction("RecordsList","Expenses");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RecordFormViewModel model)
        {
            var userId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View("ExpenseForm", model);
            }
            var expense = _recordsRepository.GetRecord(userId, model.Id);

            if (expense == null)
                return HttpNotFound();

            expense.Name = model.Name;
            expense.Description = model.Description;
            expense.Date = model.Date;
            expense.Charge = model.Charge;
            expense.Subcategory = model.Subcategory;
            expense.Category = model.Category;

            _recordsRepository.Complete();
            TempData["message"] = string.Format("Zaktualizowano {0} ", expense.Name);
            return RedirectToAction("RecordsList", "Expenses");
        }

        public ViewResult Details(int id)
        {
            var userId = User.Identity.GetUserId();

            var expense = _recordsRepository.GetRecord(userId, id);
            return View(expense);
        }
        

        public ViewResult Statistics(DateTime? dateFromParam = null, DateTime? dateToParam = null)
        {
            var userId = User.Identity.GetUserId();
            if (dateFromParam == null)
                dateFromParam = DateTime.MinValue;
            if (dateToParam == null)
                dateToParam = DateTime.MaxValue;
            var repoParam = _recordsRepository
                .GetRecordsInSelectedRange(dateFromParam, dateToParam)
                .Where(u => u.UserID == userId);

            //var repoParam = _expensesRepository.Expenses.Where(p => p.Date >= dateFromParam
            //   && p.Date <= dateToParam && p.UserID == userId);

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

        private List<object> _CreatePieSeries(IEnumerable<Record> repoParam)
        {
            var category = repoParam.Select(p => p.Category).Distinct();
            List<object> series = new List<object>();
            category.ToList().ForEach(x => series.Add(new object[] { x, repoParam.
                Where(p => p.Category == x).
                Select(p => new { p.Category, p.Charge }).
                Sum(p => p.Charge) }));
            return series;
        }
        private List<object[]> _SelectExtremeValues(IEnumerable<Record> repoParam)
        {
            List<object[]> CategoriesCharge = new List<object[]>();
            var categories = repoParam.Select(p => p.Category).Distinct();
            categories.ToList().ForEach(x => CategoriesCharge.Add(new object[] { x, repoParam.Where(p => p.Category == x).Select(p => p.Charge).Sum() }));
            return CategoriesCharge;
        }
    }
}