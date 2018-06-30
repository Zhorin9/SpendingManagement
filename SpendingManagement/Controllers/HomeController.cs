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
        private IRecordRepository _expenseRepository;
        private IApplicationUserRepository _userRepository;


        public HomeController(IRecordRepository expenseRepository, IApplicationUserRepository userRepository)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Return the view with general information about expenses and revenues of current user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ViewResult Dashboard()
        {
            var userId = User.Identity.GetUserId();
            var revenue = true;

            DashboardViewModel dashboard = new DashboardViewModel()
            {
                SumYearCharge = _expenseRepository.GetYearRecordsSum(userId, !revenue),
                SumMonthCharge = _expenseRepository.GetMonthRecordsSum(userId, !revenue),
                SumWeekCharge = _expenseRepository.GetWeekRecordsSum(userId, !revenue),
                LastTenExpenes = _expenseRepository.GetRecords(userId, 10, !revenue),

                YearRevenue = _expenseRepository.GetYearRecordsSum(userId, revenue),
                MonthRevenue = _expenseRepository.GetMonthRecordsSum(userId, revenue),
                WeekRevenue = _expenseRepository.GetWeekRecordsSum(userId, revenue),
                LastTenRevenues = _expenseRepository.GetRecords(userId, 10, revenue),
            };

            return View(dashboard);
        }

        /// <summary>
        /// Return the view with information about application
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }
    }
}