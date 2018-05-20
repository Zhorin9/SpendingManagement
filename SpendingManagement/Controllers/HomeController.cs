using Microsoft.AspNet.Identity;
using SpendingManagement.Core.Repositiories;
using SpendingManagement.Core.ViewModels;
using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace SpendingManagement.Controllers
{
    public class HomeController : Controller
    { 
        private IExpenseRepository _expenseRepository;
        private IApplicationUserRepository _userRepository;

        public HomeController(IExpenseRepository expenseRepository, IApplicationUserRepository userRepository)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }

        [Authorize]
        public ViewResult Dashboard()
        {
            var userId = User.Identity.GetUserId();

            DashboardViewModel dashboard = new DashboardViewModel()
            {
                  
            };

            dashboard.SumYearCharge = _expenseRepository.Expenses.Where(p=> p.Date.Year == DateTime.Now.Year && p.UserID == userId).Select(p => p.Charge).Sum();
            dashboard.SumMonthCharge = _expenseRepository.Expenses.Where(p => p.Date.Month == DateTime.Now.Month && p.UserID == userId)
                .Select(p => p.Charge).Sum();
            dashboard.SumWeekCharge = _expenseRepository.Expenses.Where(p => p.Date >= _GetFirstDayOfWeek().Date && p.Date <= DateTime.Now.Date && p.UserID == userId)
            .Select(p => p.Charge).Sum();
            
            dashboard.LastTenExpenes = _expenseRepository.GetExpenses(userId, 10);

            return View(dashboard);
        }

        public ActionResult About()
        {
            return View();
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
    }
}