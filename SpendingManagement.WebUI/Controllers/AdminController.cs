using SpendingManagement.Domain.Abstract;
using SpendingManagement.Domain.Entities;
using SpendingManagement.WebUI.Models;
using System;
using System.Linq;
using System.Web.Mvc;


namespace SpendingManagement.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IExpenseRepository repository;
        private IUserRepository userRepository;
        private int currentUserID;
        private int PageSize = 8;
        public AdminController(IExpenseRepository expenseRepository, IUserRepository userRepository)
        {
            repository = expenseRepository;
            this.userRepository = userRepository;
        }
        public ViewResult Index(SortingInfo sortingInfo, string sortOrder, string searchString, int page = 1)
        {
            var userId = User.Identity.GetUserId();
            CheckCurrentUserID();

            sortingInfo.DataSort = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";
            sortingInfo.NameSort = sortOrder == "name" ? "name_desc" : "name";
            sortingInfo.ChargeSort = sortOrder == "charge" ? "charge_desc" : "charge";
            sortingInfo.CategorySort = sortOrder == "category" ? "category_desc" : "category";
            sortingInfo.SubcategorySort = sortOrder == "subcategory" ? "subcategory_desc" : "subcategory";
            var parameters = repository.Expenses.Where(p => p.UserID == currentUserID);
 
            if (!String.IsNullOrEmpty(searchString))
            {
                parameters = parameters.Where(p => p.Name.Contains(searchString) || p.Category.Contains(searchString) && p.UserID == currentUserID);
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
            CheckCurrentUserID();
            Expense expense = repository.Expenses.First(p => p.ExpenseID == id && p.UserID == currentUserID);
            EditViewModel model = new EditViewModel()
            { 
                ExpenseID = expense.ExpenseID,
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
            Expense expense;
            if(model.ExpenseID == 0)
            {
                expense = new Expense();
            }
            else
            {
                expense = repository.Expenses.First(p => p.ExpenseID == model.ExpenseID);
            }
            
            expense.Name = model.Name;
            expense.Charge = model.Charge;
            expense.Category = model.Category;
            expense.Date = model.Date;
            expense.Description = model.Description;
            expense.Subcategory = model.Subcategory;
            expense.UserID = userRepository.Users.Where(p => p.Email == User.Identity.Name).Select(p => p.UserID).First();

            if (ModelState.IsValid)
            {
                repository.SaveExpense(expense);
                TempData["message"] = string.Format("Zapisano {0} ", expense.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        public ViewResult Details(int id)
        {
            Expense expense = repository.Expenses.First(p=> p.ExpenseID == id);
            return View(expense);
        }
        public ViewResult Create()
        {
            return View("Edit", new EditViewModel());
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
        [NonAction]
        private void CheckCurrentUserID()
        {
            if (currentUserID == 0)
            {
                currentUserID = userRepository.Users.Where(p => p.Email == User.Identity.Name).Select(p => p.UserID).First();
            }
        }
    }
}