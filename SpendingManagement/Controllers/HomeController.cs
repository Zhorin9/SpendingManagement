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

        [Authorize]
        public ViewResult Dashboard()
        {
            var userId = User.Identity.GetUserId();

            DashboardViewModel dashboard = new DashboardViewModel()
            {
                SumYearCharge = _expenseRepository.GetYearRecordsSum(userId, false),
                SumMonthCharge = _expenseRepository.GetMonthRecordsSum(userId, false),
                SumWeekCharge = _expenseRepository.GetWeekRecordsSum(userId, false),
                LastTenExpenes = _expenseRepository.GetRecords(userId, 10, false),

                YearRevenue = _expenseRepository.GetYearRecordsSum(userId, true),
                MonthRevenue = _expenseRepository.GetMonthRecordsSum(userId,true),
                WeekRevenue = _expenseRepository.GetWeekRecordsSum(userId, true),
                LastTenRevenues = _expenseRepository.GetRecords(userId, 10,true),
            };
            
            return View(dashboard);
        }
        
        public ActionResult About()
        {
            return View();
        }
    }
}