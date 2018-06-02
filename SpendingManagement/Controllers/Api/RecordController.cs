using Microsoft.AspNet.Identity;
using SpendingManagement.Core.Repositiories;
using System.Web.Http;

namespace SpendingManagement.Controllers.Api
{
    [Authorize]
    public class RecordController : ApiController
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IApplicationUserRepository _userRepository;

        public RecordController(IRecordRepository recordRepository, IApplicationUserRepository userRepository)
        {
            _recordRepository = recordRepository;
            _userRepository = userRepository;
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
