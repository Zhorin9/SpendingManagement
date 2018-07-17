using SpendingManagement.Core.Models;
using System.Collections.Generic;

namespace SpendingManagement.Core.Repositiories
{
    public interface IApplicationUserRepository
    {
        ApplicationUser GetUserById(string userId);

        /// <summary>
        /// Function to delete user and all dependencies form database
        /// </summary>
        void DeleteUser(ApplicationUser user);

        /// <summary>
        /// Save changes in database
        /// </summary>
        void Complete();
    }
}
