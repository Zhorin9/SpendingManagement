using Microsoft.AspNet.Identity;
using SpendingManagement.Core.Models;
using SpendingManagement.Core.Repositiories;
using SpendingManagement.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace SpendingManagement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    { 
        private IExpenseRepository _expenseRepository;
        private IApplicationUserRepository _userRepository;

        public HomeController(IExpenseRepository expenseRepository, IApplicationUserRepository userRepository)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }
        public ViewResult Index()
        {
            var userId = User.Identity.GetUserId();

            DateTimeFormatInfo info = new DateTimeFormatInfo();
            DashboardViewModel dashboard = new DashboardViewModel()
            {
                CurrentMonthName = new CultureInfo("pl").DateTimeFormat.GetMonthName(DateTime.Now.Month)
            };
            dashboard.SumYearCharge = _expenseRepository.Expenses.Where(p=> p.Date.Year == DateTime.Now.Year && p.UserID == userId).Select(p => p.Charge).Sum();
            dashboard.SumMonthCharge = _expenseRepository.Expenses.Where(p => p.Date.Month == DateTime.Now.Month && p.UserID == userId)
                .Select(p => p.Charge).Sum();
            dashboard.SumWeekCharge = _expenseRepository.Expenses.Where(p => p.Date >= _GetFirstDayOfWeek().Date && p.Date <= DateTime.Now.Date && p.UserID == userId)
            .Select(p => p.Charge).Sum();
            
            var lastTenExpenses = _expenseRepository.Expenses.Where(p=> p.UserID == userId).Reverse().OrderBy(p => p.Date).Reverse().Take(10);
            dashboard.Expsenses = lastTenExpenses;

            return View(dashboard);
        }
        private DateTime _GetFirstDayOfWeek()
        {
            var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            DateTime currentDate = DateTime.Now;
            while (currentDate.DayOfWeek != firstDayOfWeek)
            {
                currentDate = currentDate.AddDays(-1);
            }
            return currentDate;
        }
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