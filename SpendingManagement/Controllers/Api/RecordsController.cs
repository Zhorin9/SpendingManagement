using Microsoft.AspNet.Identity;
using SpendingManagement.Core.Repositiories;
using System.Web.Http;

namespace SpendingManagement.Controllers.Api
{
    [Authorize]
    public class RecordsController : ApiController
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IApplicationUserRepository _userRepository;

        public RecordsController(IRecordRepository recordRepository, IApplicationUserRepository userRepository)
        {
            _recordRepository = recordRepository;
            _userRepository = userRepository;
        }

        [HttpDelete]
        public IHttpActionResult DeleteExpense(int id)
        {
            var userId = User.Identity.GetUserId();

            var expense = _recordRepository.GetRecord(userId, id);

            if (expense == null)
                return NotFound();

            _recordRepository.DeleteExpense(expense);
            _recordRepository.Complete();
            
            return Ok();
        }
    }
}
