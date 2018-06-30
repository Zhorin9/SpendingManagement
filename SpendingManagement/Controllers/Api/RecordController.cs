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
        public string PopulateCategoriesDicitonary(bool isRevenue = false)
        {
            var serializer = new JavaScriptSerializer();

            var categoriesDictionary = _categoryRepository.GetCategoriesDictionary(isRevenue);

            var serializedResult = serializer.Serialize(categoriesDictionary);

            return serializedResult;
        }

        [HttpGet]
        public string GetChart(string categoryId, DateTime? dateFromParam = null, DateTime? dateToParam = null)
        {
            var selectedCategory ="";
            foreach (var cat in _categoryRepository.GetCategoriesList())
            {
                selectedCategory = cat;
            }
            var subcategoriesValuesList = _recordRepository
                .GetRecordsInSelectedRange(dateFromParam, dateToParam, selectedCategory);
            return selectedCategory;
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
        /*
        private List<object> _CreatePieSeries(string category, DateTime dateFromParam, DateTime dateToParam)
        {
            var subcategoriesList = _categoryRepository.GetSubcategoriesList(category);
            var subcategoriesValuesList = _recordRepository
                .GetRecordsInSelectedRange(dateFromParam, dateToParam, category);
            List<object> series = new List<object>();
            foreach (var p in subcategoriesList)
            {
                series.Add(new object[] { p, subcategoriesValuesList.
                .Where(p => p.Category == x).
                Select(p => new { p.Category, p.Charge }).
                Sum(p => p.Charge) });
            }

            return series;
            
        };
        */
        
    }
}
