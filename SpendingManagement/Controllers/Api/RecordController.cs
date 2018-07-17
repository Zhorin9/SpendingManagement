using Microsoft.AspNet.Identity;
using SpendingManagement.Core.Models;
using SpendingManagement.Core.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SpendingManagement.Controllers.Api
{
    [Authorize]
    public class RecordController : ApiController
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IApplicationUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public RecordController(IRecordRepository recordRepository, IApplicationUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _recordRepository = recordRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public IHttpActionResult PopulateCategoriesDicitonary(bool isRevenue = false)
        {
            var categoriesDictionary = _categoryRepository.GetCategoriesDictionary(isRevenue);

            if (categoriesDictionary == null)
                return NotFound();

            return Json(categoriesDictionary);
        }

        [HttpGet]
        public IHttpActionResult GetPieChart(string categoryName, DateTime? dateFromParam = null, DateTime? dateToParam = null)
        {
            var userId = User.Identity.GetUserId();
            IEnumerable<string> categoriesNameList = new List<string>();
            IEnumerable<Record> recordValuesList = new List<Record>();

            bool isSubcategory = _ChectCategoryName(categoryName);

            if (isSubcategory)
            {
                categoriesNameList = _categoryRepository.GetSubcategoriesList(categoryName);
                if (categoriesNameList == null)
                    return NotFound();
                recordValuesList = _recordRepository
                    .GetRecordsInSelectedRange(dateFromParam, dateToParam, categoryName, userId);
                if (recordValuesList == null)
                    return NotFound();
            }
            else
            {
                categoriesNameList = _categoryRepository.GetCategoriesList(false);
                recordValuesList = _recordRepository
                    .GetRecordsInSelectedRange(dateFromParam, dateToParam, false, userId);
            }

            //Dictionary used as base for creating a pie chart
            Dictionary<string, decimal> pieChartDictionary = new Dictionary<string, decimal>();

            //Add keys to the dictionary 
            foreach(var catName in categoriesNameList)
            {
                pieChartDictionary.Add(catName, 0);
            }

            if (isSubcategory)
            {
                //Adding a sum of expenses for individual subcategories.  
                foreach (var record in recordValuesList)
                {
                    pieChartDictionary[record.Subcategory] += record.Charge;
                }
            }
            else
            {
                //Adding a sum of expenses for individual general categories.  
                foreach (var record in recordValuesList)
                {
                    pieChartDictionary[record.Category] += record.Charge;
                }
            }

            return Json(pieChartDictionary);
        }
        
        [HttpGet]
        public IHttpActionResult GetLineChart(string categoryName, DateTime? dateFromParam = null, DateTime? dateToParam = null)
        {
            var userId = User.Identity.GetUserId();
            IEnumerable<Record> recordValuesList = new List<Record>();
            bool isSubcategory = _ChectCategoryName(categoryName);

            if (isSubcategory)
            {
                recordValuesList = _recordRepository
                    .GetRecordsInSelectedRange(dateFromParam, dateToParam, categoryName, userId);
                if (recordValuesList == null)
                    return NotFound();
            }
            else
            {
                recordValuesList = _recordRepository
                    .GetRecordsInSelectedRange(dateFromParam, dateToParam, false, userId);
                if (recordValuesList == null)
                    return NotFound();
            }

            //Prepare data to x serie
            IEnumerable<string> xSerie = recordValuesList
                .Select(p => p.Date.ToShortDateString())
                .Distinct();

            //Preapare values to defined data
            IEnumerable<decimal> yValues = recordValuesList
                .GroupBy(p => p.Date)
                .Select(p => p.Sum(s => s.Charge));

            List<object> lineChartData = new List<object>();
            lineChartData.Add(xSerie);
            lineChartData.Add(yValues);

            return Json(lineChartData);
        }
        

        [HttpDelete]
        public IHttpActionResult DeleteRecord(int id)
        {
            var userId = User.Identity.GetUserId();

            var record = _recordRepository.GetRecord(userId, id);

            if (record == null)
                return NotFound();

            _recordRepository.DeleteRecord(record);
            _recordRepository.Complete();
            
            return Ok();
        }     

        /// <summary>
        /// Return true if incoming value is not empty or null
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        private bool _ChectCategoryName(string categoryName)
        {
            if (categoryName != null && categoryName != "")
                return true;
            return false;
        }
    }
}
