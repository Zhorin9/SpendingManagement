using Microsoft.AspNet.Identity;
using SpendingManagement.Core.Repositiories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpendingManagement.Controllers.Api
{
    [Authorize]
    public class ExpenseController : ApiController
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IApplicationUserRepository _userRepository;

        public ExpenseController(IExpenseRepository expenseRepository, IApplicationUserRepository userRepository)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }

        [HttpDelete]
        public IHttpActionResult DeleteExpense(int id)
        {
            var userId = User.Identity.GetUserId();

            var expense = _expenseRepository.GetExpense(userId, id);

            if (expense == null)
                return NotFound();

            _expenseRepository.DeleteExpense(expense);
            _expenseRepository.Complete();
            
            return Ok();
        }
    }
}
