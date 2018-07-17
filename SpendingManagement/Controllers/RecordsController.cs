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

        public RecordsController(IRecordRepository recordsRepository, IApplicationUserRepository userRepository)
        {
            _recordsRepository = recordsRepository;
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


        /// <summary>
        /// Returns the view with the form to create a record
        /// </summary>
        /// <param name="revenue">True when create revenue/false when expense</param>
        /// <returns></returns>
        public ViewResult Create(bool revenue)
        {
            var form = new RecordFormViewModel();
            if (revenue)
            {
                form.Heading = "Dodaj przychód";
                form.IsRevenue = true;
            }
            else
            {
                form.Heading = "Stwórz wydatek";
                form.IsRevenue = false;
            }
            return View("RecordForm", form);
        }

        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var record = _recordsRepository.GetRecord(userId, id);

            if (record == null)
                return HttpNotFound();

            RecordFormViewModel model = new RecordFormViewModel()
            {
                Heading = "Edycja - " + record.Name,
                Id = record.Id,
                Name = record.Name,
                Charge = record.Charge,
                Category = record.Category,
                Date = record.Date,
                Description = record.Description,
                Subcategory = record.Subcategory,
            };
            return View("RecordForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RecordFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("RecordForm", model);
            }

            var record = new Record
            {
                UserID = User.Identity.GetUserId(),
                IsRevenue = model.IsRevenue,
                Date = model.Date,
                Charge = model.Charge,
                Category = model.Category,
                Subcategory = model.Subcategory,
                Name = model.Name
            };

            _recordsRepository.AddRecord(record);
            _recordsRepository.Complete();

            TempData["message"] = string.Format("Zapisano {0} ", record.Name);
            return RedirectToAction("RecordsList","Records");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RecordFormViewModel model)
        {
            var userId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return View("RecordForm", model);
            }
            var record = _recordsRepository.GetRecord(userId, model.Id);

            if (record == null)
                return HttpNotFound();

            record.Name = model.Name;
            record.IsRevenue = model.IsRevenue;
            record.Description = model.Description;
            record.Date = model.Date;
            record.Charge = model.Charge;
            record.Subcategory = model.Subcategory;
            record.Category = model.Category;

            _recordsRepository.Complete();
            TempData["message"] = string.Format("Zaktualizowano {0} ", record.Name);
            return RedirectToAction("RecordsList", "Records");
        }

        /// <summary>
        /// Returns the view with details about the record
        /// </summary>
        /// <param name="id">Unique id record</param>
        /// <returns></returns>
        public ViewResult Details(int id)
        {
            var userId = User.Identity.GetUserId();

            var record = _recordsRepository.GetRecord(userId, id);
            return View(record);
        }
        
        public ViewResult Statistics(DateTime? dateFromParam = null, DateTime? dateToParam = null)
        {
            var userId = User.Identity.GetUserId();

            var repoParam = _recordsRepository
                .GetRecordsInSelectedRange(dateFromParam, dateToParam, false, userId)
                .Where(u => u.UserID == userId);

            StatisticsViewModel statistics = new StatisticsViewModel()
            {
                SumCharge = repoParam.Sum(p => p.Charge),
                CategoriesCharge = _SelectExtremeValues(repoParam),
            };
            return View(statistics);
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