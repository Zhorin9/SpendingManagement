using SpendingManagement.Core.Models;
using System.Collections.Generic;

namespace SpendingManagement.Core.Repositiories
{
    public interface IApplicationUserRepository
    {
        IEnumerable<ApplicationUser> GetUserById(string userId);
    }
}
