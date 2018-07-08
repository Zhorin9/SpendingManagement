using Microsoft.AspNet.Identity;
using SpendingManagement.Core.Repositiories;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Collections.Generic;
using System;
using SpendingManagement.Core.Models;

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
            IEnumerable<string> categoriesNameList = new List<string>();
            IEnumerable<Record> recordValuesList = new List<Record>();
            bool isSubcategory = false;

            //If the categoryName is empty or null, the data will be preapared for general categories
            if (categoryName != null && categoryName != "")
                isSubcategory = true;

            if (isSubcategory)
            {
                categoriesNameList = _categoryRepository.GetSubcategoriesList(categoryName);
                if (categoriesNameList == null)
                    return NotFound();
                recordValuesList = _recordRepository
                    .GetRecordsInSelectedRange(dateFromParam, dateToParam, categoryName);
                if (recordValuesList == null)
                    return NotFound();
            }
            else
            {
                categoriesNameList = _categoryRepository.GetCategoriesList(false);
                recordValuesList = _recordRepository
                    .GetRecordsInSelectedRange(dateFromParam, dateToParam, false);
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
    }
}
