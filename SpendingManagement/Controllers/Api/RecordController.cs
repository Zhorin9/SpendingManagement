using Microsoft.AspNet.Identity;
using SpendingManagement.Core.Repositiories;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Web.Http.Results;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

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
